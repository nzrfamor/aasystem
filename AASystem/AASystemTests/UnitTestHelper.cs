using DAL.Data;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace AASystemTests
{
    internal static class UnitTestHelper
    {
        public static DbContextOptions<AASystemDbContext> GetUnitTestDbOptions()
        {
            var options = new DbContextOptionsBuilder<AASystemDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new AASystemDbContext(options))
            {
                SeedData(context);
            }

            return options;
        }

        private static void SeedData (AASystemDbContext context)
        {
            context.Persons.AddRange(
                new Person { Id = 1, Name = "Name1", Surname = "Surname1", BirthDate = new DateTime(1991, 6, 10) },
                new Person { Id = 2, Name = "Name2", Surname = "Surname2", BirthDate = new DateTime(1996, 10, 19) });
            context.Customers.AddRange(
                new Customer { Id = 1, PersonId = 1, Role = Roles.user, EmailAddress = "name1@gmail.com", Password = "person1psswrd", PhoneNumber = "+380000000001" },
                new Customer { Id = 2, PersonId = 2, Role = Roles.user, EmailAddress = "name2@gmail.com", Password = "person2psswrd", PhoneNumber = "+380000000002" });
            context.SaveChanges();
        }
    }

}