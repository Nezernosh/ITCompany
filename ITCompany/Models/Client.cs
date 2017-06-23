using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITCompany.Models
{
    public class Client
    {
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Contact { get; set; }
    }
}