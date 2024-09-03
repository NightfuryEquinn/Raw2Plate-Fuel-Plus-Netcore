using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Raw2PlateFuelPlusNetcore.Models;

namespace Raw2PlateFuelPlusNetcore.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PlannerController : ControllerBase
  {
    private readonly RawDBContext _context;
    public PlannerController(RawDBContext context)
    {
      _context = context;
    }

    // GET: api/planner
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Planner>>> GetPlanners()
    {
      var _plannerList = await _context.Planners.ToListAsync();

      return Ok(_plannerList);
    }

    // GET: api/planner/1
    [HttpGet("{id}")]
    public async Task<ActionResult<Planner>> GetPlanner(int id)
    {
      var _planner = await _context.Planners.FindAsync(id);

      if (_planner == null)
      {
        return NotFound();
      }

      return Ok(_planner);
    }

    // POST: api/planner
    [HttpPost]
    public async Task<ActionResult<Planner>> PostPlanner(Planner _planner)
    {
      _context.Planners.Add(_planner);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetPlanner", new { id = _planner.PlannerId }, _planner);
    }

    // PUT: api/planner/1
    [HttpPut]
    public async Task<IActionResult> PutPlanner(int id, Planner _planner)
    {
      if (id != _planner.PlannerId)
      {
        return BadRequest();
      }

      _context.Entry(_planner).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!PlannerExists(id))
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

    // DELETE: api/planner/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlanner(int id)
    {
      var _planner = await _context.Planners.FindAsync(id);

      if (_planner == null)
      {
        return NotFound();
      }

      _context.Planners.Remove(_planner);
      await _context.SaveChangesAsync();

      return Ok();
    }

    private bool PlannerExists(int id)
    {
      return _context.Planners.Any(e => e.PlannerId == id);
    }
  }
}
