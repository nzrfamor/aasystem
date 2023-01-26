using DAL.Entities;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AASystemTests
{
    internal class PersonEqualityComparer : IEqualityComparer<Person>
    {
        public bool Equals([AllowNull] Person x, [AllowNull] Person y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.Name == y.Name
                && x.Surname == y.Surname
                && x.BirthDate == y.BirthDate;
        }

        public int GetHashCode([DisallowNull] Person obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class CustomerEqualityComparer : IEqualityComparer<Customer>
    {
        public bool Equals([AllowNull] Customer x, [AllowNull] Customer y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.PersonId == y.PersonId
                && x.EmailAddress == y.EmailAddress
                && x.PhoneNumber == y.PhoneNumber
                && x.Password == y.Password
                && x.Role == y.Role;
        }

        public int GetHashCode([DisallowNull] Customer obj)
        {
            return obj.GetHashCode();
        }
    }
}
