using System;
using System.Collections.Generic;
using System.Text;

namespace PluseDemoProject.Areas.ViewModel
{
    public class UserResponseModel
    {
        public int inUserId { get; set; }
        public string stFirstName { get; set; }
        public string stLastName { get; set; }
        public string stEmailAddress { get; set; }
        public DateTime dtCreationDate { get; set; }
        //public int Id { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string EmailAddress { get; set; }
        //public DateTime CreationDate { get; set; }
    }
}
