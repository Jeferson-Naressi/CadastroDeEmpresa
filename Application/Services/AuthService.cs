using Business.Interfaces.Repository;
using Business.Interfaces.Services;
using CadastroDeEmpresa.DTOs;
using CadastroDeEmpresa.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

        public async Task<string> UserRegister(RegistrarUsuarioDTO registrarUsuarioDTO)
        {
            // Verifica se o e-mail já está cadastrado
            var exists = await _authRepository.UserExistsAsync(registrarUsuarioDTO.Email);

            if (exists) throw new Exception("Já existe um usuário com esse e-mail.");

            // Gera um hash seguro da senha com BCrypt
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(registrarUsuarioDTO.Senha);

            // Cria o objeto Usuario com os dados informados
            var user = new Usuario
            {
                Nome = registrarUsuarioDTO.Nome,
                Email = registrarUsuarioDTO.Email,
                SenhaHash = passwordHash
            };

            await _authRepository.SaveUserAsync(user);

            return  "Usuário registrado com sucesso!";
        }

        public async Task<string> UserLogin(LoginUsuarioDTO loginUsuarioDTO)
        {
            // Busca o usuário pelo e-mail
            var usuario = await _authRepository.GetUserFindByEmail(loginUsuarioDTO.Email);

            // Se não encontrar ou a senha for inválida, retorna 401
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(loginUsuarioDTO.Senha, usuario.SenhaHash))
                throw new Exception("Credenciais inválidas.");

            // Gera o JWT para o usuário autenticado
            var token = GerarToken(usuario);

            return token;
        }

        // Método interno que gera o JWT
        private string GerarToken(Usuario usuario)
        {
            // Informações (claims) que vão dentro do token
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email)
            };

            // Gera a chave secreta a partir do appsettings.json
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            // Define o tipo de criptografia
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Monta o token JWT com tempo de expiração e assinaturas
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            // Serializa o token em string para ser retornado
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
