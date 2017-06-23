using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITCompany.Models
{
    public class Project
    {
        public int ID { get; set; }
        [Required]
        public int ClientID { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public decimal Budget { get; set; }
    }
}