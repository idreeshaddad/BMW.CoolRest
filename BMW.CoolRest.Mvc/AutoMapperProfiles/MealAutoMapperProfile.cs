using AutoMapper;
using BMW.CoolRest.Mvc.Data.Entities;
using BMW.CoolRest.Mvc.Models.Meals;

namespace BMW.CoolRest.Mvc.AutoMapperProfiles
{
    public class MealAutoMapperProfile : Profile
    {
        public MealAutoMapperProfile()
        {
            CreateMap<Meal, MealListViewModel>();
            CreateMap<Meal, MealDetailsViewModel>();
            CreateMap<CreateUpdateMealViewModel, Meal>();

            CreateMap<Meal, CreateUpdateMealViewModel>()
                .ForMember(dest => dest.IngredientIds, 
                           opts => opts.MapFrom(src => src.Ingredients.Select(ing => ing.Id).ToList()));

        }
    }
}
