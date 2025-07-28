using Business.DTOs;
using Business.Interfaces.Repositories;
using Business.Interfaces.Services;
using Business.Models;
using System.Text.Json;

namespace Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly CnpjLookupService _cnpjLookupService;

        public CompanyService(ICompanyRepository companyRepository, CnpjLookupService cnpjLookupService)
        {
            _companyRepository = companyRepository;
            _cnpjLookupService = cnpjLookupService;
        }

        public async Task<string> UserRegister(RegisterCompanyDTO registerCompanyDTO, Guid userId)
        {
            var json = await _cnpjLookupService.ConsultarCnpjAsync(registerCompanyDTO.CNPJ);

            if (json == null)
                throw new Exception("Failed to fetch CNPJ data. Make sure it is valid or try again later.");

            var root = json.RootElement;

            string cnpj = GetRequiredField(root, "cnpj");
            string companyName = GetRequiredField(root, "nome");
            string status = GetRequiredField(root, "situacao");

            var company = new Company
            {
                CompanyName = companyName,
                FantasyName = GetSafeString(root, "fantasia"),
                CNPJ = cnpj,
                Status = status,
                OpeningDate = GetSafeString(root, "abertura"),
                Type = GetSafeString(root, "tipo"),
                LegalNature = GetSafeString(root, "natureza_juridica"),
                MainActivity = root.TryGetProperty("atividade_principal", out var activities) &&
                               activities.GetArrayLength() > 0 &&
                               activities[0].TryGetProperty("text", out var activity)
                               ? activity.GetString() ?? "" : "",
                Street = GetSafeString(root, "logradouro"),
                Number = GetSafeString(root, "numero"),
                Complement = GetSafeString(root, "complemento"),
                Neighborhood = GetSafeString(root, "bairro"),
                City = GetSafeString(root, "municipio"),
                State = GetSafeString(root, "uf"),
                PostalCode = GetSafeString(root, "cep"),
                UserId = userId
            };

            await _companyRepository.AddAsync(company);
            return "Company registered successfully!";
        }

        public async Task<List<CompanyDTO>> ListUserCompanies(Guid userId)
        {
            var companies = await _companyRepository.GetCompaniesByUser(userId);

            return companies.Select(c => new CompanyDTO
            {
                CompanyName = c.CompanyName,
                TradeName = c.FantasyName,
                CNPJ = c.CNPJ,
                Status = c.Status,
                OpeningDate = c.OpeningDate,
                Type = c.Type,
                LegalNature = c.LegalNature,
                MainActivity = c.MainActivity,
                Street = c.Street,
                Number = c.Number,
                Complement = c.Complement,
                Neighborhood = c.Neighborhood,
                City = c.City,
                State = c.State,
                PostalCode = c.PostalCode
            }).ToList();
        }

        private string GetRequiredField(JsonElement root, string fieldName)
        {
            if (!root.TryGetProperty(fieldName, out var element) || string.IsNullOrWhiteSpace(element.GetString()))
            {
                throw new Exception($"Missing or empty required field: {fieldName}");
            }

            return element.GetString()!;
        }

        private string GetSafeString(JsonElement root, string field)
        {
            return root.TryGetProperty(field, out var prop) ? prop.GetString() ?? "" : "";
        }
    }
}
