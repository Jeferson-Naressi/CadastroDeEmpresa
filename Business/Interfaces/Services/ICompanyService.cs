using Business.DTOs;

namespace Business.Interfaces.Services
{
    public interface ICompanyService
    {
        Task<string> UserRegister(RegisterCompanyDTO registerCompanyDTO, Guid userId);
        Task<List<CompanyDTO>> ListUserCompanies(Guid userId);
    }
}
