using Business.Interfaces.Repository;
using CadastroDeEmpresa.Data;
using CadastroDeEmpresa.DTOs;
using CadastroDeEmpresa.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly EmpresaContext _context;

        public AuthRepository(EmpresaContext context)
        {
            _context = context;
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            var data = await _context.Usuarios.AnyAsync(u => u.Email == email);

            return data;
        }

        public async Task SaveUserAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);

            await _context.SaveChangesAsync();
        }

        public async Task<Usuario> GetUserFindByEmail(string email)
        {
            var data = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

            return data!;
        }
    }
}
