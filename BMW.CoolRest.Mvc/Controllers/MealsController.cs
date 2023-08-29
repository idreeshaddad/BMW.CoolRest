using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BMW.CoolRest.Mvc.Data;
using BMW.CoolRest.Mvc.Data.Entities;
using AutoMapper;
using BMW.CoolRest.Mvc.Models.Meals;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BMW.CoolRest.Mvc.Controllers
{
    public class MealsController : Controller
    {
        #region Data and Const

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MealsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #endregion

        #region Actions

        public async Task<IActionResult> Index()
        {
            var meals = await _context
                                .Meals
                                .ToListAsync();

            var mealVMs = _mapper.Map<List<MealListViewModel>>(meals);

            return View(mealVMs);
        }

        public async Task<IActionResult> Details(int? id) 
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context
                                .Meals
                                .Include(meal => meal.Ingredients)
                                .Where(meal => meal.Id == id)
                                .SingleOrDefaultAsync();
                                
            if (meal == null)
            {
                return NotFound(); // 404
            }

            var mealVM = _mapper.Map<MealDetailsViewModel>(meal);

            return View(mealVM);
        }

        public IActionResult Create()
        {
            var vm = new CreateUpdateMealViewModel();
            vm.IngredientsMultiselectList = new MultiSelectList(_context.Ingredients, "Id", "Name");

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUpdateMealViewModel mealVM)
        {
            if (ModelState.IsValid)
            {
                var meal = _mapper.Map<Meal>(mealVM);

                await UpdateMealIngredients(meal, mealVM.IngredientIds);

                _context.Add(meal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            mealVM.IngredientsMultiselectList = new MultiSelectList(_context.Ingredients, "Id", "Name", mealVM.IngredientIds);
            return View(mealVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context
                                .Meals
                                .Include(meal => meal.Ingredients)
                                .Where(meal => meal.Id == id)
                                .SingleOrDefaultAsync();

            if (meal == null)
            {
                return NotFound();
            }

            var mealVM = _mapper.Map<CreateUpdateMealViewModel>(meal);

            // mealVM.IngredientIds = meal.Ingredients.Select(ing => ing.Id).ToList();
            // Commented out because AutoMapper was informed how to extract Ids from the Enitity

            mealVM.IngredientsMultiselectList = new MultiSelectList(_context.Ingredients, "Id", "Name", mealVM.IngredientIds);

            return View(mealVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Meal meal)
        {
            if (id != meal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MealExists(meal.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(meal);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Meals == null)
            {
                return NotFound();
            }

            var meal = await _context.Meals
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meal == null)
            {
                return NotFound();
            }

            return View(meal);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Meals == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Meals'  is null.");
            }
            var meal = await _context.Meals.FindAsync(id);
            if (meal != null)
            {
                _context.Meals.Remove(meal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Private Functions

        private bool MealExists(int id)
        {
            return (_context.Meals?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private async Task UpdateMealIngredients(Meal meal, List<int> ingredientIds) // 11, 12
        {
            var ingredients = await _context
                                        .Ingredients
                                        .Where(ing => ingredientIds.Contains(ing.Id))
                                        .ToListAsync();

            meal.Ingredients.AddRange(ingredients);
        }

        #endregion
    }
}
