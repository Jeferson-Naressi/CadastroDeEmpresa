using Microsoft.EntityFrameworkCore;

namespace CadastroDeEmpresa.Models
{
    public class Usuario 
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public String Email { get; set; } = string.Empty;
        public string SenhaHash { get; set; } = string.Empty;

        //public List<Empresa> Empresas { get; set; } = new ();

    }
}
