using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Raw2PlateFuelPlusNetcore.Models;

namespace Raw2PlateFuelPlusNetcore.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class GroceryListController : ControllerBase
  {
    private readonly RawDBContext _context;
    public GroceryListController(RawDBContext context)
    {
      _context = context;
    }

    // GET: api/grocerylist
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GroceryList>>> GetGroceryLists()
    {
      var _grocerylistList = await _context.GroceryLists.ToListAsync();

      return Ok(_grocerylistList);
    }

    // GET: api/grocerylist/1
    [HttpGet("{id}")]
    public async Task<ActionResult<GroceryList>> GetGroceryList(int id)
    {
      var _grocerylist = await _context.GroceryLists.FindAsync(id);

      if (_grocerylist == null)
      {
        return NotFound();
      }

      return Ok(_grocerylist);
    }

    // POST: api/grocerylist/1
    [HttpPost]
    public async Task<ActionResult<GroceryList>> PostGroceryList(GroceryList _grocerylist)
    {
      _context.GroceryLists.Add(_grocerylist);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetGroceryList", new { id = _grocerylist.GroceryListId }, _grocerylist);
    }

    // PUT: api/grocerylist/1
    [HttpPut]
    public async Task<IActionResult> PutGroceryList(int id, GroceryList _grocerylist)
    {
      if (id != _grocerylist.GroceryListId)
      {
        return BadRequest();
      }

      _context.Entry(_grocerylist).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!GroceryListExists(id))
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

    // DELETE: api/grocerylist/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGroceryList(int id)
    {
      var _grocerylist = await _context.GroceryLists.FindAsync(id);

      if (_grocerylist == null)
      {
        return NotFound();
      }

      _context.GroceryLists.Remove(_grocerylist);
      await _context.SaveChangesAsync();

      return Ok();
    }

    private bool GroceryListExists(int id)
    {
      return _context.GroceryLists.Any(e => e.GroceryListId == id);
    }
  }
}
