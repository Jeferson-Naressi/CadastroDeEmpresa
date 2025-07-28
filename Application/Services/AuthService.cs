using Business.Interfaces.Services;
using Business.DTOs;
using Business.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Business.Interfaces.Repositories;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }

        public async Task<string> UserRegister(RegisterUserDTO registrarUsuarioDTO)
        {
            var exists = await _authRepository.UserExistsAsync(registrarUsuarioDTO.Email);

            if (exists) throw new Exception("Já existe um usuário com esse e-mail.");


            var passwordHash = BCrypt.Net.BCrypt.HashPassword(registrarUsuarioDTO.Password);

            var user = new User
            {
                Name = registrarUsuarioDTO.Name,
                Email = registrarUsuarioDTO.Email,
                PasswordHash = passwordHash
            };

            await _authRepository.SaveUserAsync(user);

            return  "Usuário registrado com sucesso!";
        }

        public async Task<string> UserLogin(LoginUserDTO loginUserDTO)
        {
            var user = await _authRepository.GetUserFindByEmail(loginUserDTO.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginUserDTO.Senha, user.PasswordHash))
                throw new Exception("Credenciais inválidas.");

            var token = GenerateToken(user);

            return token;
        }

        private string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
