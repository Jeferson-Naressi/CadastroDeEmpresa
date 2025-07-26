using Microsoft.EntityFrameworkCore;
using CadastroDeEmpresa.Models;

namespace CadastroDeEmpresa.Data
{
    public class EmpresaContext : DbContext
    {
        public EmpresaContext(DbContextOptions<EmpresaContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Empresa> Empresas { get; set; } = null!;
    }
}
