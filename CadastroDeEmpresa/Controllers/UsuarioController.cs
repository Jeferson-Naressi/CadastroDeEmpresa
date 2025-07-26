using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using CadastroDeEmpresa.DTOs;


namespace CadastroDeEmpresa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        // Rota protegida: GET /api/usuario/me
        [HttpGet("me")]
        [Authorize] // Requer autenticação
        public ActionResult<InfoUsuarioDTO> Getme ()
        {
            var nome = User.Identity?.Name; // Obtém o nome do usuário autenticado
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value; // Obtém o e-mail do usuário autenticado
           
            var resposta = new InfoUsuarioDTO
            {
                Mensagem = "Acesso Autorizado!",
                Nome = nome ?? "", // Se o nome for nulo, define um valor padrão
                Email = email ?? "" // Se o e-mail for nulo, define um valor padrão
            };
            return Ok (resposta); // Retorna o objeto DTO com as informações do usuário autenticado
        }
    }
    }
