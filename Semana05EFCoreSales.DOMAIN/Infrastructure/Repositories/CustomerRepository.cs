using Microsoft.EntityFrameworkCore;
using Semana05EFCoreSales.DOMAIN.Core.Entities;
using Semana05EFCoreSales.DOMAIN.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semana05EFCoreSales.DOMAIN.Infrastructure.Repositories
{
    public class CustomerRepository
    {
        private readonly SalesUESANContext _context;

        public CustomerRepository(SalesUESANContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetCustomers() 
        {
            var customers = await _context.Customer.ToListAsync();
            return customers;
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            var customer = await _context.Customer.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (customer == null)
                throw new Exception("Customer not found");

            return customer;
        }

        public async Task<bool> Insert(Customer customer)
        {
            await _context.Customer.AddAsync(customer);
            var countRows = await _context.SaveChangesAsync();
            return countRows > 0;
        }

        public async Task<bool> Update(Customer customer)
        {
            _context.Customer.Update(customer);
            var countRows = await _context.SaveChangesAsync();
            return countRows > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
                return false;

            _context.Customer.Remove(customer);
            var countRows = await _context.SaveChangesAsync();
            return countRows > 0;
        }

    }
}
