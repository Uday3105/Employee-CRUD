using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeCRUD.Models
{
    public class Employee
    {
        [Display(Name = "Id")]
        public int Empid { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }
        public int Gender { get; set; }
        [Required(ErrorMessage ="Languages Known is required.")]
        public string LanguagesKnown { get; set; }
        public int AddressId { get; set; }

        public string Address { get; set; }

        [Required(ErrorMessage = "Salary is required.")]
        public int Salary { get; set; }
        public bool IsActive { get; set; }

        public Address A { get; set; }

    }
}