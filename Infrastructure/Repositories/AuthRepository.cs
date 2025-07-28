using Infrastructure.Data;
using Business.DTOs;
using Business.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces.Repositories;

namespace Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly CompanyContext _context;

        public AuthRepository(CompanyContext context)
        {
            _context = context;
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            var data = await _context.User.AnyAsync(u => u.Email == email);

            return data;
        }

        public async Task SaveUserAsync(User user)
        {
            _context.User.Add(user);

            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserFindByEmail(string email)
        {
            var data = await _context.User.FirstOrDefaultAsync(u => u.Email == email);

            return data!;
        }
    }
}
