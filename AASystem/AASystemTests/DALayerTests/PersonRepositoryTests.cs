﻿using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AASystemTests.DALayerTests
{
    [TestFixture]
    public class PersonRepositoryTests
    {

        [TestCase(1)]
        [TestCase(2)]
        public async Task PersonRepository_GetByIdAsync_ReturnsSingleValue(int id)
        {
            //Arrange
            var context = new AASystemDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var personRepository = new PersonRepository(context);
            //Act
            var person = await personRepository.GetByIdAsync(id);
            var expectedPerson = expectedPersons.FirstOrDefault(p => p.Id == id);

            //Assert
            Assert.That(expectedPerson, Is.EqualTo(person).Using(new PersonEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }



        [Test]
        public async Task PersonRepository_GetAllAsync_ReturnsAllValues()
        {
            //Arrange
            var context = new AASystemDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var personRepository = new PersonRepository(context);
            //Act
            var persons = await personRepository.GetAllAsync();

            //Assert
            Assert.That(expectedPersons, Is.EqualTo(persons).Using(new PersonEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]
        public async Task PersonRepository_AddAsync_AddsValueToDatabase()
        {
            //Arrange
            var context = new AASystemDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var personRepository = new PersonRepository(context);
            //Act
            await personRepository.AddAsync(new Person
            {
                Id = 3
            });
            await context.SaveChangesAsync();

            //Assert
            Assert.That(context.Persons.Count(), Is.EqualTo(3), message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task PersonRepository_DeleteByIdAsync_DeletesEntity()
        {
            //Arrange
            var context = new AASystemDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var personRepository = new PersonRepository(context);
            //Act
            await personRepository.DeleteByIdAsync(1);
            await context.SaveChangesAsync();

            //Assert
            Assert.That(context.Persons.Count(), Is.EqualTo(1), message: "DeleteByIdAsync works incorrect");
        }

        [Test]
        public async Task PersonRepository_Update_UpdatesEntity()
        {
            //Arrange
            var context = new AASystemDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var personRepository = new PersonRepository(context);
            //Act
            var person = new Person
            {
                Id = 1,
                Name = "Name_Updated",
                Surname = "Surname_Updated",
                BirthDate = new DateTime(1980, 7, 25)
            };
            personRepository.Update(person);
            await context.SaveChangesAsync();

            var expectedPerson = await personRepository.GetByIdAsync(1);

            await context.SaveChangesAsync();

            Assert.That(expectedPerson, Is.EqualTo(person).Using(new PersonEqualityComparer()), message: "Update method works incorrect");
        }

        readonly IEnumerable<Person> expectedPersons = new[]
        {
            new Person { Id = 1, Name = "Name1", Surname = "Surname1", BirthDate = new DateTime(1991, 6, 10) },
            new Person { Id = 2, Name = "Name2", Surname = "Surname2", BirthDate = new DateTime(1996, 10, 19) }

        };
    }
}
