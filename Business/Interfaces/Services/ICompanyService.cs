using CadastroDeEmpresa.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces.Services
{
    public interface ICompanyService
    {
        public Task<string> UserRegister(RegisterCompanyDTO registerCompanyDTO);

        public Task<List<RegisterCompanyDTO>> ListUserCompanies(Guid usuarioId);
    }
}
