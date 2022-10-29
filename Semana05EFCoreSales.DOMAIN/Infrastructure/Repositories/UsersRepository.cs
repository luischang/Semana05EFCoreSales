using Microsoft.EntityFrameworkCore;
using Semana05EFCoreSales.DOMAIN.Core.Entities;
using Semana05EFCoreSales.DOMAIN.Core.Interfaces;
using Semana05EFCoreSales.DOMAIN.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semana05EFCoreSales.DOMAIN.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly SalesUESANContext _context;

        public UsersRepository(SalesUESANContext context)
        {
            _context = context;
        }

        public async Task<Users> Login(string email, string password)
        {
            var auth = await _context
                        .Users
                        .Where(x => x.Email == email
                            && x.Password == password
                            && x.IsActive == true).FirstOrDefaultAsync();
            return auth;
        }
    }
}
