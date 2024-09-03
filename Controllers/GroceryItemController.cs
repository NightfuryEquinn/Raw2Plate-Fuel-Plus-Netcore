using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Raw2PlateFuelPlusNetcore.Models;

namespace Raw2PlateFuelPlusNetcore.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class GroceryItemController : ControllerBase
  {
    private readonly RawDBContext _context;
    public GroceryItemController(RawDBContext context)
    {
      _context = context;
    }

    // GET: api/groceryitem
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GroceryItem>>> GetGroceryItems()
    {
      var _groceryitemList = await _context.GroceryItems.ToListAsync();

      return Ok(_groceryitemList);
    }

    // GET: api/groceryitem/1
    [HttpGet("{id}")]
    public async Task<ActionResult<GroceryItem>> GetGroceryItem(int id)
    {
      var _groceryitem = await _context.GroceryItems.FindAsync(id);

      if (_groceryitem == null)
      {
        return NotFound();
      }

      return Ok(_groceryitem);
    }

    // POST: api/groceryitem
    [HttpPost]
    public async Task<ActionResult<GroceryItem>> PostGroceryItem(GroceryItem _groceryitem)
    {
      _context.GroceryItems.Add(_groceryitem);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetGroceryItem", new { id = _groceryitem.GroceryItemId }, _groceryitem);
    }

    // PUT: api/groceryitem/1
    [HttpPut]
    public async Task<IActionResult> PutGroceryItem(int id, GroceryItem _groceryitem)
    {
      if (id != _groceryitem.GroceryItemId)
      {
        return BadRequest();
      }

      _context.Entry(_groceryitem).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!GroceryItemExists(id))
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

    // DELETE: api/groceryitem/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGroceryItem(int id)
    {
      var _groceryitem = await _context.GroceryItems.FindAsync(id);

      if (_groceryitem == null)
      {
        return NotFound();
      }

      _context.GroceryItems.Remove(_groceryitem);
      await _context.SaveChangesAsync();

      return Ok();
    }

    private bool GroceryItemExists(int id)
    {
      return _context.GroceryItems.Any(e => e.GroceryItemId == id);
    }
  }
}
