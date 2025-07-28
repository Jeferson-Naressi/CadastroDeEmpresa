using Business.Models;

namespace Business.Interfaces.Repositories
{
    public interface IAuthRepository
    {
        public Task<bool> UserExistsAsync(string email);
        public Task SaveUserAsync(User user);
        public Task<User> GetUserFindByEmail(string email);
    }
}
