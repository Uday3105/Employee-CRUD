using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeCRUD.Models
{
    public class EmpModel
    {
        [Display(Name = "Id")]
        public int Empid { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
    }
}