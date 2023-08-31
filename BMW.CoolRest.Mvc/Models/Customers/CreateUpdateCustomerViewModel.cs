using BMW.CoolRest.Mvc.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BMW.CoolRest.Mvc.Models.Customers
{
    public class CreateUpdateCustomerViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Fisrt Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }


        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public Gender Gender { get; set; }

        [ValidateNever]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
    }
}
