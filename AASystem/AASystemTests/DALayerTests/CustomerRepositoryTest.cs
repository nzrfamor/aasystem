using DAL.Data;
using DAL.Entities;
using DAL.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AASystemTests.DALayerTests
{
    [TestFixture]
    public class CustomerRepositoryTest
    {
        [TestCase(1)]
        [TestCase(2)]
        public async Task CustomerRepository_GetByIdAsync_ReturnsSingleValue(int id)
        {
            //Arrange
            using var context = new AASystemDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var customerRepository = new CustomerRepository(context);
            //Act
            var customer = await customerRepository.GetByIdAsync(id);
            var expectedCustomer = ExpectedCustomers.FirstOrDefault(x => x.Id == id);
            //Assert
            Assert.That(expectedCustomer, Is.EqualTo(customer).Using(new CustomerEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }

        [Test]
        public async Task CustomerRepository_GetAllAsync_ReturnsAllValues()
        {
            //Arrange
            using var context = new AASystemDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var customerRepository = new CustomerRepository(context);
            //Act
            var customers = await customerRepository.GetAllAsync();
            //Assert
            Assert.That(ExpectedCustomers, Is.EqualTo(customers).Using(new CustomerEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]
        public async Task CustomerRepository_AddAsync_AddsValueToDatabase()
        {
            //Arrange
            using var context = new AASystemDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var customerRepository = new CustomerRepository(context);
            //Act
            var customer = new Customer { Id = 3 };
            await customerRepository.AddAsync(customer);
            await context.SaveChangesAsync();
            //Arrange
            Assert.That(context.Customers.Count(), Is.EqualTo(3), message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task CustomerRepository_DeleteByIdAsync_DeletesEntity()
        {
            //Arrange
            using var context = new AASystemDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var customerRepository = new CustomerRepository(context);
            //Act
            await customerRepository.DeleteByIdAsync(1);
            await context.SaveChangesAsync();
            //Assert
            Assert.That(context.Customers.Count(), Is.EqualTo(1), message: "DeleteByIdAsync works incorrect");
        }

        [Test]
        public async Task CustomerRepository_Update_UpdatesEntity()
        {
            //Arrange
            using var context = new AASystemDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var customerRepository = new CustomerRepository(context);
            //Act
            var customer = new Customer
            {
                Id = 1,
                PersonId = 1,
                Role = Roles.user,
                EmailAddress = "newname1@gmail.com",
                Password = "newperson1psswrd",
                PhoneNumber = "+380000000001"

            };

            customerRepository.Update(customer);
            await context.SaveChangesAsync();
            //Assert
            Assert.That(customer, Is.EqualTo(new Customer
            {
                Id = 1,
                PersonId = 1,
                Role = Roles.user,
                EmailAddress = "newname1@gmail.com",
                Password = "newperson1psswrd",
                PhoneNumber = "+380000000001"

            }).Using(new CustomerEqualityComparer()), message: "Update method works incorrect");
        }

        [Test]
        public async Task CustomerRepository_GetByIdWithDetailsAsync_ReturnsWithIncludedEntities()
        {
            //Arrange
            using var context = new AASystemDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var customerRepository = new CustomerRepository(context);
            //Act
            var customer = await customerRepository.GetByIdWithDetailsAsync(1);
            var expectedCustomer = ExpectedCustomers.FirstOrDefault(x => x.Id == 1);
            //Assert
            Assert.That(customer,
                Is.EqualTo(expectedCustomer).Using(new CustomerEqualityComparer()), message: "GetByIdWithDetailsAsync method works incorrect");

            Assert.That(customer.Person,
                Is.EqualTo(ExpectedPersons.FirstOrDefault(x => x.Id == expectedCustomer.PersonId)).Using(new PersonEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");
        }

        [Test]
        public async Task CustomerRepository_GetAllWithDetailsAsync_ReturnsWithIncludedEntities()
        {
            //Arrange
            using var context = new AASystemDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var customerRepository = new CustomerRepository(context);
            //Act
            var customers = await customerRepository.GetAllWithDetailsAsync();

            Assert.That(customers,
                Is.EqualTo(ExpectedCustomers).Using(new CustomerEqualityComparer()), message: "GetAllWithDetailsAsync method works incorrect");

            Assert.That(customers.Select(i => i.Person).OrderBy(i => i.Id),
                Is.EqualTo(ExpectedPersons).Using(new PersonEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");
        }

        private static IEnumerable<Person> ExpectedPersons =>
           new[]
           {
            new Person { Id = 1, Name = "Name1", Surname = "Surname1", BirthDate = new DateTime(1991, 6, 10) },
            new Person { Id = 2, Name = "Name2", Surname = "Surname2", BirthDate = new DateTime(1996, 10, 19) }
           };

        private static IEnumerable<Customer> ExpectedCustomers =>
            new[]
            {
                new Customer { Id = 1, PersonId = 1, Role = Roles.user, EmailAddress = "name1@gmail.com", Password = "person1psswrd", PhoneNumber = "+380000000001" },
                new Customer { Id = 2, PersonId = 2, Role = Roles.user, EmailAddress = "name2@gmail.com", Password = "person2psswrd", PhoneNumber = "+380000000002" }
            };
    }
}
