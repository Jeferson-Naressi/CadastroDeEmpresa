using Business.DTOs;

namespace Business.Interfaces.Services
{
    public interface IAuthService
    {
        public Task<string> UserRegister(RegisterUserDTO registrarUsuarioDTO);

        public Task<string> UserLogin(LoginUserDTO loginUsuarioDTO);
    }
}
