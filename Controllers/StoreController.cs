using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Raw2PlateFuelPlusNetcore.Models;

namespace Raw2PlateFuelPlusNetcore.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class StoreController : ControllerBase
  {
    private readonly RawDBContext _context;
    public StoreController(RawDBContext context)
    {
      _context = context;
    }

    // GET: api/store
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Store>>> GetStores()
    {
      var _storeList = await _context.Stores.ToListAsync();

      return Ok(_storeList);
    }

    // GET: api/store/1
    [HttpGet("{id}")]
    public async Task<ActionResult<Store>> GetStore(int id)
    {
      var _store = await _context.Stores.FindAsync(id);

      if (_store == null)
      {
        return NotFound();
      }

      return Ok(_store);
    }

    // POST: api/store/1
    [HttpPost]
    public async Task<ActionResult<Store>> PostStore(Store _store)
    {
      _context.Stores.Add(_store);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetStore", new { id = _store.StoreId }, _store);
    }

    // PUT: api/store/1
    [HttpPut]
    public async Task<IActionResult> PutStore(int id, Store _store)
    {
      if (id != _store.StoreId)
      {
        return BadRequest();
      }

      _context.Entry(_store).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!StoreExists(id))
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

    // DELETE: api/store/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStore(int id)
    {
      var _store = await _context.Stores.FindAsync(id);

      if (_store == null)
      {
        return NotFound();
      }

      _context.Stores.Remove(_store);
      await _context.SaveChangesAsync();

      return Ok();
    }

    private bool StoreExists(int id)
    {
      return _context.Stores.Any(e => e.StoreId == id);
    }
  }
}
