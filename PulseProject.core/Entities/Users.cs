using System;
using System.Collections.Generic;
using System.Text;

namespace PulseDemo.core.Entities
{
    public class Users
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public DateTime CreationDate {get;set;}

    }
}
