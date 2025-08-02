using Business.Interfaces.Services;
using Infrastructure.Data;
using Business.DTOs;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers
{
    [ApiController] 
    [Route("api/[controller]")] 
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authservice;

        public UserController(IAuthService authService)
        {
            _authservice = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Registrar(RegisterUserDTO dto)
        {
            var response = await _authservice.UserRegister(dto);

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDTO dto)
        {
            var response = await _authservice.UserLogin(dto);

            return Ok(response);
        }
    }
}
