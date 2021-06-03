using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeCRUD.ViewModel
{
    public class EmployeeViewModel
    {
        public class EmpModel
        {
            [Display(Name = "Id")]
            public int Empid { get; set; }
            [Required(ErrorMessage = "First name is required.")]
            public string FirstName { get; set; }
            [Required(ErrorMessage = "Last name is required.")]
            public string LastName { get; set; }
            public int Gender { get; set; }
            [Required(ErrorMessage = "Languages Known is required.")]
            public string LanguagesKnown { get; set; }
            public int Address { get; set; }
            [Required(ErrorMessage = "Salary is required.")]
            public int Salary { get; set; }
            public bool IsActive { get; set; }
        }

        public class Address
        {
            public int Id { get; set; }
            public string Address_1 { get; set; }
            public string Address_2 { get; set; }
            public int CountryId { get; set; }
            public int StateId { get; set; }
            public int DistrictId { get; set; }
            [Required(ErrorMessage = "City is required.")]
            public string City { get; set; }
        }
    }
}