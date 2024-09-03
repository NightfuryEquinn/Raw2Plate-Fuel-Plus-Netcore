using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Raw2PlateFuelPlusNetcore.Models;

namespace Raw2PlateFuelPlusNetcore.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class MealController : ControllerBase
  {
    private readonly RawDBContext _context;
    public MealController(RawDBContext context)
    {
      _context = context;
    }

    // GET: api/meal
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Meal>>> GetMeals()
    {
      var _mealList = await _context.Meals.ToListAsync();

      return Ok(_mealList);
    }

    // GET: api/meal/1
    [HttpGet("{id}")]
    public async Task<ActionResult<Meal>> GetMeal(int id)
    {
      var _meal = await _context.Meals.FindAsync(id);

      if (_meal == null)
      {
        return NotFound();
      }

      return Ok(_meal);
    }

    // POST: api/meal
    [HttpPost]
    public async Task<ActionResult<Meal>> PostMeal(Meal _meal)
    {
      _context.Meals.Add(_meal);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetMeal", new { id = _meal.MealId }, _meal);
    }

    // PUT: api/meal/1
    [HttpPut]
    public async Task<IActionResult> PutMeal(int id, Meal _meal)
    {
      if (id != _meal.MealId)
      {
        return BadRequest();
      }

      _context.Entry(_meal).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!MealExists(id))
        {
          return NotFound();
        }
        else
        {
          return BadRequest();
        }
      }

      return Ok();
    }

    // DELETE: api/meal/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMeal(int id)
    {
      var _meal = await _context.Meals.FindAsync(id);

      if (_meal == null)
      {
        return NotFound();
      }

      _context.Meals.Remove(_meal);
      await _context.SaveChangesAsync();

      return Ok();
    }

    private bool MealExists(int id)
    {
      return _context.Meals.Any(e => e.MealId == id);
    }
  }
}
