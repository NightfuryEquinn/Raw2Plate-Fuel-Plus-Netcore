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

    // GET: api/meal/planner/1
    [HttpGet("planner/{id}")]
    public async Task<ActionResult<IEnumerable<Meal>>> GetMealPlannerByUserId(int id)
    {
      var _meal = await (from meal in _context.Meals
                         join planner in _context.Planners on meal.PlannerId equals planner.PlannerId
                         where planner.UserId == id
                         select new
                         {
                           meal.MealId,
                           meal.MealType,
                           meal.RecipeId,
                           planner.PlannerId,
                           planner.Date,
                           planner.UserId
                         }).ToListAsync();

      if (_meal == null)
      {
        return BadRequest();
      }

      return Ok(_meal);
    }

    // GET: api/meal/tracker/1
    [HttpGet("tracker/{id}")]
    public async Task<ActionResult<IEnumerable<Meal>>> GetMealTrackerByUserId(int id)
    {
      var _meal = await (from meal in _context.Meals
                         join tracker in _context.Trackers on meal.TrackerId equals tracker.TrackerId
                         where tracker.UserId == id
                         select new
                         {
                           meal.MealId,
                           meal.MealType,
                           meal.RecipeId,
                           tracker.TrackerId,
                           tracker.Date,
                           tracker.UserId
                         }).ToListAsync();

      if (_meal == null)
      {
        return BadRequest();
      }

      return Ok(_meal);
    }

    // POST: api/meal/user/1/2024-09-14
    [HttpPost("user/{id}/{date}")]
    public async Task<ActionResult<Meal>> PostMeal(int id, string date, Meal _meal)
    {
      // Check existing meal on same meal type
      var _existingMeal = await _context.Meals
        .FirstOrDefaultAsync(meal => meal.MealType == _meal.MealType && meal.RecipeId == _meal.RecipeId);

      if (_existingMeal != null)
      {
        return BadRequest();
      }

      // Check existing planner of the user
      var _planner = await _context.Planners
        .FirstOrDefaultAsync(planner => planner.UserId == id && planner.Date == date);

      if (_planner == null)
      {
        _planner = new Planner
        {
          PlannerId = 0,
          Date = date,
          UserId = id
        };

        _context.Planners.Add(_planner);
        await _context.SaveChangesAsync();
      }

      // Check existing tracker of the user
      var _tracker = await _context.Trackers
        .FirstOrDefaultAsync(tracker => tracker.UserId == id && tracker.Date == date);

      if (_tracker == null)
      {
        _tracker = new Tracker
        {
          TrackerId = 0,
          Date = date,
          UserId = id
        };

        _context.Trackers.Add(_tracker);
        await _context.SaveChangesAsync();
      }

      _meal.PlannerId = _planner.PlannerId;
      _meal.TrackerId = _tracker.TrackerId;

      _context.Meals.Add(_meal);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetMeal", new { id = _meal.MealId }, _meal);
    }

    // PUT: api/meal/1
    [HttpPut("{id}")]
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
          return Unauthorized();
        }
        else
        {
          return NotFound();
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
