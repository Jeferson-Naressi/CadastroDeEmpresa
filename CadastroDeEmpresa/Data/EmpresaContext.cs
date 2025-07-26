using Microsoft.EntityFrameworkCore;

namespace CadastroDeEmpresa.Data
{
    public class EmpresaContext : DbContext
    {
        public EmpresaContext(DbContextOptions<EmpresaContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; } = null!;
    }
}
