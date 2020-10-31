using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PulseDemo.Application.ViewModel
{
    public class UserModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Enter LastName")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Enter BirthDate")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Enter EmailAddress")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        public string Password { get; set; }

        public DateTime? CreationDate { get; set; }
    }
}
