using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Raw2PlateFuelPlusNetcore.Models;

namespace Raw2PlateFuelPlusNetcore.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ManualMealController : ControllerBase
  {
    private readonly RawDBContext _context;
    public ManualMealController(RawDBContext context)
    {
      _context = context;
    }

    // GET: api/manualmeal
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ManualMeal>>> GetManualMeals()
    {
      var _manualmealList = await _context.ManualMeals.ToListAsync();

      return Ok(_manualmealList);
    }

    // GET: api/manualmeal/1
    [HttpGet("{id}")]
    public async Task<ActionResult<ManualMeal>> GetManualMeal(int id)
    {
      var _manualmeal = await _context.ManualMeals.FindAsync(id);

      if (_manualmeal == null)
      {
        return NotFound();
      }

      return Ok(_manualmeal);
    }

    // POST: api/manualmeal/1
    [HttpPost]
    public async Task<ActionResult<ManualMeal>> PostManualMeal(ManualMeal _manualmeal)
    {
      _context.ManualMeals.Add(_manualmeal);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetManualMeal", new { id = _manualmeal.ManualMealId }, _manualmeal);
    }

    // PUT: api/manualmeal/1
    [HttpPut]
    public async Task<IActionResult> PutManualMeal(int id, ManualMeal _manualmeal)
    {
      if (id != _manualmeal.ManualMealId)
      {
        return BadRequest();
      }

      _context.Entry(_manualmeal).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ManualMealExists(id))
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

    // DELETE: api/manualmeal/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteManualMeal(int id)
    {
      var _manualmeal = await _context.ManualMeals.FindAsync(id);

      if (_manualmeal == null)
      {
        return NotFound();
      }

      _context.ManualMeals.Remove(_manualmeal);
      await _context.SaveChangesAsync();

      return Ok();
    }

    private bool ManualMealExists(int id)
    {
      return _context.ManualMeals.Any(e => e.ManualMealId == id);
    }
  }
}
