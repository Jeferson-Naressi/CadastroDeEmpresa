// Importações necessárias
using CadastroDeEmpresa.Data; // DbContext (EmpresaContext)
using CadastroDeEmpresa.DTOs; // DTOs de login e registro
using CadastroDeEmpresa.Models; // Entidade Usuario
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt; // Para gerar JWT
using System.Security.Claims; // Para gerar as "reivindicações" do usuário
using Microsoft.IdentityModel.Tokens; // Criptografia do token
using System.Text;


namespace CadastroDeEmpresa.Controllers
{
    [ApiController] // Indica que esse é um controller de API
    [Route("api/[controller]")] // Define a rota base: /api/auth
    public class AuthController : ControllerBase
    {
        private readonly EmpresaContext _context; // Nosso DbContext
        private readonly IConfiguration _configuration; // Acesso ao appsettings.json

        // Construtor com injeção de dependência
        public AuthController(EmpresaContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // Rota: POST /api/auth/registrar
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar(RegisterUsuarioDTO dto)
        {
            // Verifica se o e-mail já está cadastrado
            var existe = await _context.Usuarios.AnyAsync(u => u.Email == dto.Email);
            if (existe)
                return BadRequest(new { message = "Já existe um usuário com esse e-mail." });

            // Gera um hash seguro da senha com BCrypt
            var senhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha);

            // Cria o objeto Usuario com os dados informados
            var usuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                SenhaHash = senhaHash
            };

            // Salva o novo usuário no banco
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Usuário registrado com sucesso!" });
        }

        // Rota: POST /api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUsuarioDTO dto)
        {
            // Busca o usuário pelo e-mail
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == dto.Email);

            // Se não encontrar ou a senha for inválida, retorna 401
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.SenhaHash))
                return Unauthorized(new { message = "Credenciais inválidas." });

            // Gera o JWT para o usuário autenticado
            var token = GerarToken(usuario);
            return Ok(new { token }); // Retorna o token para o front-end
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
