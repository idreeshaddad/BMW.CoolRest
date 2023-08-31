using BMW.CoolRest.Mvc.Enums;

namespace BMW.CoolRest.Mvc.Models.Customers
{
    public class CustomerDetailsViewModel
    {
        public CustomerDetailsViewModel()
        {
            //Orders = new List<OrderListViewModel>();
        }

        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }

        //public List<OrderListViewModel> Orders { get; set; }
    }
}
