using AutoMapper;
using BMW.CoolRest.Mvc.Data.Entities;
using BMW.CoolRest.Mvc.Models.Customers;

namespace BMW.CoolRest.Mvc.AutoMapperProfiles
{
    public class CustomerAutoMapperProfile : Profile
    {
        public CustomerAutoMapperProfile()
        {
            CreateMap<Customer, CustomerListViewModel>();
            CreateMap<Customer, CustomerDetailsViewModel>();
            CreateMap<Customer, CreateUpdateCustomerViewModel>().ReverseMap();

        }
    }
}
