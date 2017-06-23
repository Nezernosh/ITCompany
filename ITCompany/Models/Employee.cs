using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITCompany.Models
{
    public class Employee
    {
        public int ID { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        [StringLength (12)]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Position { get; set; }
    }
}