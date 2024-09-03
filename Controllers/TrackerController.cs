using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Raw2PlateFuelPlusNetcore.Models;

namespace Raw2PlateFuelPlusNetcore.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TrackerController : ControllerBase
  {
    private readonly RawDBContext _context;
    public TrackerController(RawDBContext context)
    {
      _context = context;
    }

    // GET: api/tracker
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tracker>>> GetTrackers()
    {
      var _trackerList = await _context.Trackers.ToListAsync();

      return Ok(_trackerList);
    }

    // GET: api/tracker/1
    [HttpGet("{id}")]
    public async Task<ActionResult<Tracker>> GetTracker(int id)
    {
      var _tracker = await _context.Trackers.FindAsync(id);

      if (_tracker == null)
      {
        return NotFound();
      }

      return Ok(_tracker);
    }

    // POST: api/tracker
    [HttpPost]
    public async Task<ActionResult<Tracker>> PostTracker(Tracker _tracker)
    {
      _context.Trackers.Add(_tracker);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetTracker", new { id = _tracker.TrackerId }, _tracker);
    }

    // PUT: api/tracker/1
    [HttpPut]
    public async Task<IActionResult> PutTracker(int id, Tracker _tracker)
    {
      if (id != _tracker.TrackerId)
      {
        return BadRequest();
      }

      _context.Entry(_tracker).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!TrackerExists(id))
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

    // DELETE: api/tracker/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTracker(int id)
    {
      var _tracker = await _context.Trackers.FindAsync(id);

      if (_tracker == null)
      {
        return NotFound();
      }

      _context.Trackers.Remove(_tracker);
      await _context.SaveChangesAsync();

      return Ok();
    }

    private bool TrackerExists(int id)
    {
      return _context.Trackers.Any(e => e.TrackerId == id);
    }
  }
}
