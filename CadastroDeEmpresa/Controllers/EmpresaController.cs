using CadastroDeEmpresa.Data;
using CadastroDeEmpresa.DTOs;
using CadastroDeEmpresa.Models;
using CadastroDeEmpresa.Servicos ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;


namespace CadastroDeEmpresa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly EmpresaContext _context;
        private readonly ReceitaWsServico _receitaServico; 

        public EmpresaController(EmpresaContext context, ReceitaWsServico receitaWsServico)
        {
            _context = context;
            _receitaServico = receitaWsServico;
        }
        private string ObterCampoObrigatorio(JsonElement root, string nomeCampo)
        {
            if (!root.TryGetProperty(nomeCampo, out var elemento) || string.IsNullOrWhiteSpace(elemento.GetString()))
            {
                throw new Exception($"Campo obrigatório ausente ou vazio: {nomeCampo}");
            }

            return elemento.GetString()!;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CadastrarEmpresa([FromBody] CadastrarEmpresaDTO dto)
        {
            try
            {
                var json = await _receitaServico.ConsultarCnpjAsync(dto.Cnpj);

                if (json == null)
                    return BadRequest(new { erro = "Erro ao consultar o CNPJ. Verifique se ele é válido ou tente novamente mais tarde." });

                var root = json.RootElement;

                // Valida campos essenciais
                string cnpj = ObterCampoObrigatorio(root, "cnpj");
                string nomeEmpresarial = ObterCampoObrigatorio(root, "nome");
                string situacao = ObterCampoObrigatorio(root, "situacao");

                var empresa = new Empresa
                {
                    NomeEmpresarial = nomeEmpresarial,
                    NomeFantasia = root.TryGetProperty("fantasia", out var fantasia) ? fantasia.GetString() ?? "" : "",
                    Cnpj = cnpj,
                    Situacao = situacao,
                    Abertura = root.TryGetProperty("abertura", out var abertura) ? abertura.GetString() ?? "" : "",
                    Tipo = root.TryGetProperty("tipo", out var tipo) ? tipo.GetString() ?? "" : "",
                    NaturezaJuridica = root.TryGetProperty("natureza_juridica", out var natureza) ? natureza.GetString() ?? "" : "",
                    AtividadePrincipal = root.TryGetProperty("atividade_principal", out var atividade) && atividade.GetArrayLength() > 0
                        && atividade[0].TryGetProperty("text", out var text)
                            ? text.GetString() ?? "" : "",
                    Logradouro = root.TryGetProperty("logradouro", out var log) ? log.GetString() ?? "" : "",
                    Numero = root.TryGetProperty("numero", out var num) ? num.GetString() ?? "" : "",
                    Complemento = root.TryGetProperty("complemento", out var comp) ? comp.GetString() ?? "" : "",
                    Bairro = root.TryGetProperty("bairro", out var bairro) ? bairro.GetString() ?? "" : "",
                    Municipio = root.TryGetProperty("municipio", out var mun) ? mun.GetString() ?? "" : "",
                    UF = root.TryGetProperty("uf", out var uf) ? uf.GetString() ?? "" : "",
                    Cep = root.TryGetProperty("cep", out var cep) ? cep.GetString() ?? "" : "",
                    UsuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)
                };

                _context.Empresas.Add(empresa);
                await _context.SaveChangesAsync();

                return Ok(new { mensagem = "Empresa cadastrada com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = "Erro ao cadastrar empresa", detalhe = ex.Message });
            }
        }
    }
}
