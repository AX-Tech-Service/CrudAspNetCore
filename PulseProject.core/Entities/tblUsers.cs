using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PulseDemo.core.Entities
{
    public class tblUsers
    {
        [Key]
        public int inUserId { get; set; }
        [StringLength(50)]
        public string stFirstName { get; set; }
        [StringLength(50)]
        public string stLastName { get; set; }
        public DateTime dtBirthDate { get; set; }
        [StringLength(50)]
        public string stEmailAddress { get; set; }
        [StringLength(50)]
        public string stPassword { get; set; }
        public DateTime dtCreationDate { get; set; }
    }
}
