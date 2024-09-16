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

    // GET: api/manualmeal/tracker/1
    [HttpGet("tracker/{id}")]
    public async Task<ActionResult<IEnumerable<ManualMeal>>> GetManualMealTrackerByUserId(int id)
    {
      var _manualMeal = await (from mMeal in _context.ManualMeals
                               join tracker in _context.Trackers on mMeal.TrackerId equals tracker.TrackerId
                               where tracker.UserId == id
                               select new
                               {
                                 mMeal.ManualMealId,
                                 mMeal.Name,
                                 mMeal.Calories,
                                 tracker.TrackerId,
                                 tracker.Date,
                                 tracker.UserId
                               }).ToListAsync();

      if (_manualMeal == null)
      {
        return BadRequest();
      }

      return Ok(_manualMeal);
    }

    // POST: api/manualmeal/user/1/2024-09-14
    [HttpPost("user/{id}/{date}")]
    public async Task<ActionResult<ManualMeal>> PostManualMeal(int id, string date, ManualMeal _manualmeal)
    {
      // Check existing tracker of the user
      var _tracker = await _context.Trackers
        .FirstOrDefaultAsync(mMeal => mMeal.UserId == id && mMeal.Date == date);

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

      _manualmeal.TrackerId = _tracker.TrackerId;

      _context.ManualMeals.Add(_manualmeal);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetManualMeal", new { id = _manualmeal.ManualMealId }, _manualmeal);
    }

    // PUT: api/manualmeal/1
    [HttpPut("{id}")]
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
          return Unauthorized();
        }
        else
        {
          return NotFound();
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
