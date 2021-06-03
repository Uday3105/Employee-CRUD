using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeCRUD.Models
{
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