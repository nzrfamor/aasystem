using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    internal class UnitOfWork : IUnitOfWork
    {
        readonly AASystemDbContext db;
        ICustomerRepository customerRepository;
        IPersonRepository personRepository;
        public UnitOfWork(AASystemDbContext _db)
        {
            db = _db;
        }
        public ICustomerRepository CustomerRepository
        {
            get
            {
                return customerRepository ??= new CustomerRepository(db);
            }
        }

        public IPersonRepository PersonRepository
        {
            get
            {
                return personRepository ??= new PersonRepository(db);
            }
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
