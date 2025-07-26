using CadastroDeEmpresa.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces.Services
{
    public interface IAuthService
    {
        public Task<string> UserRegister(RegistrarUsuarioDTO registrarUsuarioDTO);

        public Task<string> UserLogin(LoginUsuarioDTO loginUsuarioDTO);
    }
}
