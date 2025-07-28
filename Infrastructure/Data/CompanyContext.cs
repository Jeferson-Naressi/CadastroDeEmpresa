using Business.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class CompanyContext : DbContext
    {

        public CompanyContext(DbContextOptions<CompanyContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; } = null!;
        public DbSet<Company> Companies { get; set; } = null!;
    }
}
