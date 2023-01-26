using DAL.Entities.AbstractEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public enum Roles
    {
        user,

        admin
    }
    public class Customer : BaseEntity
    {
        public int PersonId { get; set; }
        public Roles Role { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public Person Person { get; set; }    
    }
}
