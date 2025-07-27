using Business.Interfaces.Services;
using CadastroDeEmpresa.Data;
using CadastroDeEmpresa.DTOs;
using Microsoft.AspNetCore.Mvc;


namespace CadastroDeEmpresa.Controllers
{
    [ApiController] // Indica que esse é um controller de API
    [Route("api/[controller]")] // Define a rota base: /api/auth
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authservice;

        // Construtor com injeção de dependência
        public AuthController(IAuthService authService)
        {
            _authservice = authService;
        }

        // Rota: POST /api/auth/registrar
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar(RegisterUserDTO dto)
        {
            var response = await _authservice.UserRegister(dto);

            return Ok(response);
        }

        // Rota: POST /api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUsuarioDTO dto)
        {
            var response = await _authservice.UserLogin(dto);

            return Ok(response);
        }
    }
}
