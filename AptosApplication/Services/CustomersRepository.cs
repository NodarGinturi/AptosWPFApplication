using Aptos.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AptosApplication.Services
{
    public class CustomersRepository : ICustomerRepository
    {
        AptosDbContext _context = new AptosDbContext();

        public Task<List<Customer>> GetCustomersAsync()
        {
            foreach(var a in _context.Customers)
            {
                a.BirthDate = new DateTime(a.BirthDateYear, a.BirthDateMonth, a.BirthDateDay).ToShortDateString();
            }
            return _context.Customers.AsNoTracking().ToListAsync();
        }


        public Task<Customer> GetCustomerAsync(Guid id) => _context.Customers.FirstOrDefaultAsync(c => c.Id == id);

        public async Task<Customer> AddCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            if (!_context.Customers.Any(c => c.Id == customer.Id))
                _context.Customers.Attach(customer);

            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return customer;
        }

        public async void DeleteCustomerAsync(Customer customer)
        {
            if (_context.Customers.Any(c => c.Id == customer.Id))
            {
                _context.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }
    }
}
