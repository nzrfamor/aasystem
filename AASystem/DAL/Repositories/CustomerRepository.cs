using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        readonly AASystemDbContext db;
        public CustomerRepository(AASystemDbContext _db)
        {
            db = _db;
        }

        public Task AddAsync(Customer entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Customer entity) 
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Customer>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Customer>> GetAllWithDetailsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await db.Customers.FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task<Customer> GetByIdWithDetailsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
