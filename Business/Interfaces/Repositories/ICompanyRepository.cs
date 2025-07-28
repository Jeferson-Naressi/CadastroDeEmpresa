using Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces.Repositories
{
    public interface ICompanyRepository
    {
        Task AddAsync(Company company);
        Task<List<Company>> GetCompaniesByUser(Guid userId);
    }
}
