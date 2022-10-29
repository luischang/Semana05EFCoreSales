using Semana05EFCoreSales.DOMAIN.Core.Entities;

namespace Semana05EFCoreSales.DOMAIN.Core.Interfaces
{
    public interface IUsersRepository
    {
        Task<Users> Login(string email, string password);
    }
}