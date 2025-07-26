using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace CadastroDeEmpresa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        // Rota protegida: GET /api/usuario/me
        [HttpGet("me")]
        [Authorize] // Requer autenticação
        public IActionResult Getme ()
        {
            var nome = User.Identity?.Name; // Obtém o nome do usuário autenticado
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value; // Obtém o e-mail do usuário autenticado
            return Ok(new
            {
                mensagem = "Acesso Autorizado!",
                nome,
                email
            });
        }
    }
    }
