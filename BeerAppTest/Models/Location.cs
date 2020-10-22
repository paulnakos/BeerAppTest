using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BeerAppTest.Models
{
    public class Location
    {
        public int LocationID { get; set; }

        [Required]
        public string Country { get; set; }
        public int? PostalCode { get; set; }
        public string Address { get; set; }

        
        

        public virtual ICollection<User> Users { get; set; } // or virtual User User ??
    }
}