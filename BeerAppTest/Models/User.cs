using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeerAppTest.Models
{
    public class User
    {
        public int UserID { get; set; }
		public Gender? Gender { get; set; }

		[StringLength(20,MinimumLength = 3)]
		public string LastName { get; set; }

		[StringLength(20, MinimumLength = 3)]
		public string FirstName { get; set; }

		[StringLength(20, MinimumLength = 3)]
		public string UserName { get; set; }

		[DataType(DataType.EmailAddress)]
		public string EmailAdress { get; set; }


		[DataType(DataType.Password)]
		public string Password { get; set; }


	    [DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		[Display(Name = "Birth Day")]
		public DateTime BirtDate { get; set; }

		public int? LocationID { get; set; }
		public string FullName  // calculated property
		{
			get
			{
				return FirstName + " " + LastName;
			}
		}

       
		public virtual Location Location { get; set; }
    }
}