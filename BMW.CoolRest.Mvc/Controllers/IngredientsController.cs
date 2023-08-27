using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BMW.CoolRest.Mvc.Data;
using BMW.CoolRest.Mvc.Data.Entities;
using AutoMapper;
using BMW.CoolRest.Mvc.Models.Ingredients;

namespace BMW.CoolRest.Mvc.Controllers
{
    public class IngredientsController : Controller
    {
        #region Data and Const

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public IngredientsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #endregion

        #region Actions

        public async Task<IActionResult> Index()
        {
            var ingredients = await _context
                                .Ingredients
                                .ToListAsync();

            var ingVMs = _mapper.Map<List<IngredientViewModel>>(ingredients);

            return View(ingVMs);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var ingredient = await _context
                                        .Ingredients
                                        .FirstOrDefaultAsync(m => m.Id == id);

            if (ingredient == null)
            {
                return NotFound();
            }

            var ingVM = _mapper.Map<IngredientViewModel>(ingredient);

            return View(ingVM);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IngredientViewModel ingredientVM)
        {
            if (ModelState.IsValid)
            {
                var ing = _mapper.Map<Ingredient>(ingredientVM);
                // OR
                // var ing = _mapper.Map<IngredientViewModel, Ingredient>(ingredientVM);

                _context.Add(ing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(ingredientVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredient = await _context.Ingredients.FindAsync(id);

            if (ingredient == null)
            {
                return NotFound();
            }

            var ingVM = _mapper.Map<IngredientViewModel>(ingredient);

            return View(ingVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IngredientViewModel ingredientVM)
        {
            if (id != ingredientVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var ing = _mapper.Map<Ingredient>(ingredientVM);

                try
                {
                    _context.Update(ing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngredientExists(ingredientVM.Id))
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

            return View(ingredientVM);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ingredients == null)
            {
                return NotFound();
            }

            var ingredient = await _context.Ingredients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingredient == null)
            {
                return NotFound();
            }

            return View(ingredient);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ingredients == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Ingredients'  is null.");
            }
            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient != null)
            {
                _context.Ingredients.Remove(ingredient);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Private Functions

        private bool IngredientExists(int id)
        {
            return (_context.Ingredients?.Any(e => e.Id == id)).GetValueOrDefault();
        } 

        #endregion
    }
}
