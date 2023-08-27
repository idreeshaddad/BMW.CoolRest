using AutoMapper;
using BMW.CoolRest.Mvc.Data.Entities;
using BMW.CoolRest.Mvc.Models.Ingredients;

namespace BMW.CoolRest.Mvc.AutoMapperProfiles
{
    public class IngredientAutoMapperProfile : Profile
    {
        public IngredientAutoMapperProfile()
        {
            CreateMap<Ingredient, IngredientViewModel>().ReverseMap();
        }
    }
}
