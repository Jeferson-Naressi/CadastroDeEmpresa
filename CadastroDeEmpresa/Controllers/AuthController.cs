using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Business.DTOs;


namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpGet("Authenticate")]
        [Authorize]
        public IActionResult GetMe()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var name = User.FindFirstValue(ClaimTypes.Name);
            var email = User.FindFirstValue(ClaimTypes.Email);

            return Ok(new
            {
                Mensagem = "Token válido!",
                UserId = userId,
                Name = name,
                Email = email
            });
        }
    }
}