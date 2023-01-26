using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Data
{
    public class AASystemDbContext : DbContext
    {
        public AASystemDbContext(DbContextOptions<AASystemDbContext> options) : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.Person)
                .WithOne()
                .HasForeignKey<Customer>(p => p.PersonId);
        }
    }
}
