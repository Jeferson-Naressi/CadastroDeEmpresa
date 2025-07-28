using Infrastructure.Data;
using Business.Models;
using Microsoft.EntityFrameworkCore;
using Business.Interfaces.Services;
using Business.Interfaces.Repositories;

namespace Infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly CompanyContext _context;

        public CompanyRepository(CompanyContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Company company)
        {
            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Company>> GetCompaniesByUser(Guid userId)
        {
            return await _context.Companies
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }
    }
}
