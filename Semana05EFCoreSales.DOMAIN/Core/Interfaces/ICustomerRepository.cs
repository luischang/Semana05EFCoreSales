using Semana05EFCoreSales.DOMAIN.Core.Entities;

namespace Semana05EFCoreSales.DOMAIN.Core.Interfaces
{
    public interface ICustomerRepository
    {
        Task<bool> Delete(int id);
        Task<Customer> GetCustomerById(int id);
        Task<IEnumerable<Customer>> GetCustomers();
        Task<bool> Insert(Customer customer);
        Task<bool> Update(Customer customer);
    }
}