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

        public async Task AddAsync(Customer entity)
        {
            await db.Customers.AddAsync(entity);
        }

        public void Delete(Customer entity) 
        {
            db.Customers.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            db.Customers.Remove(await db.Customers.FirstOrDefaultAsync(c => c.Id == id));
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await db.Customers.ToListAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllWithDetailsAsync()
        {
            return await db.Customers.Include(c => c.Person).ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await db.Customers.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Customer> GetByIdWithDetailsAsync(int id)
        {
            return await db.Customers.Include(c => c.Person).FirstOrDefaultAsync(c => c.Id == id);
        }

        public void Update(Customer entity)
        {
            db.Customers.Update(entity);
        }
    }
}
