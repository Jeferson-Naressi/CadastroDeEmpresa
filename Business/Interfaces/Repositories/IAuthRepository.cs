using CadastroDeEmpresa.Models;

namespace Business.Interfaces.Repository
{
    public interface IAuthRepository
    {
        public Task<bool> UserExistsAsync(string email);
        public Task SaveUserAsync(Usuario usuario);
        public Task<Usuario> GetUserFindByEmail(string email);
    }
}
