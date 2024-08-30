using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Raw2PlateFuelPlusNetcore.Models;

namespace Raw2PlateFuelPlusNetcore.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ItemController : ControllerBase
  {
    private readonly RawDBContext _context;
    public ItemController(RawDBContext context)
    {
      _context = context;
    }

    // GET: api/item
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Item>>> GetItems()
    {
      var _itemList = await _context.Items.ToListAsync();

      return Ok(_itemList);
    }

    // GET: api/item/1
    [HttpGet("{id}")]
    public async Task<ActionResult<Item>> GetItem(int id)
    {
      var _item = await _context.Items.FindAsync(id);

      if (_item == null)
      {
        return NotFound();
      }

      return Ok(_item);
    }

    // POST: api/item/1
    [HttpPost]
    public async Task<ActionResult<Item>> PostItem(Item _item)
    {
      _context.Items.Add(_item);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetItem", new { id = _item.ItemId }, _item);
    }

    // PUT: api/item/1
    [HttpPut]
    public async Task<IActionResult> PutItem(int id, Item _item)
    {
      if (id != _item.ItemId)
      {
        return BadRequest();
      }

      _context.Entry(_item).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ItemExists(id))
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

    // DELETE: api/item/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteItem(int id)
    {
      var _item = await _context.Items.FindAsync(id);

      if (_item == null)
      {
        return NotFound();
      }

      _context.Items.Remove(_item);
      await _context.SaveChangesAsync();

      return Ok();
    }

    private bool ItemExists(int id)
    {
      return _context.Items.Any(e => e.ItemId == id);
    }
  }
}
