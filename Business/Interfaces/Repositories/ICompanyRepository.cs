using Business.Models;

namespace Business.Interfaces.Repositories
{
    public interface ICompanyRepository
    {
        Task AddAsync(Company company);
        Task<List<Company>> GetCompaniesByUser(Guid userId);
    }
}
