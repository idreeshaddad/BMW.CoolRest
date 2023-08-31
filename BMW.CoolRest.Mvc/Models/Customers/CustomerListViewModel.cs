using BMW.CoolRest.Mvc.Enums;
using System.ComponentModel.DataAnnotations;

namespace BMW.CoolRest.Mvc.Models.Customers
{
    public class CustomerListViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public Gender Gender { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        public int Age { get; set; }
    }
}
