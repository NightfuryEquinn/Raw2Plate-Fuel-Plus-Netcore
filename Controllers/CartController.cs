using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Raw2PlateFuelPlusNetcore.Models;

namespace Raw2PlateFuelPlusNetcore.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CartController : ControllerBase
  {
    private readonly RawDBContext _context;
    public CartController(RawDBContext context)
    {
      _context = context;
    }

    // GET: api/cart
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()
    {
      var _cartList = await _context.Carts.ToListAsync();

      return Ok(_cartList);
    }

    // GET: api/cart/1
    [HttpGet("{id}")]
    public async Task<ActionResult<Cart>> GetCart(int id)
    {
      var _cart = await _context.Carts.FindAsync(id);

      if (_cart == null)
      {
        return NotFound();
      }

      return Ok(_cart);
    }

    // POST: api/cart/1
    [HttpPost]
    public async Task<ActionResult<Cart>> PostCart(Cart _cart)
    {
      _context.Carts.Add(_cart);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetCart", new { id = _cart.CartId }, _cart);
    }

    // PUT: api/cart/1
    [HttpPut]
    public async Task<IActionResult> PutCart(int id, Cart _cart)
    {
      if (id != _cart.CartId)
      {
        return BadRequest();
      }

      _context.Entry(_cart).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!CartExists(id))
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

    // DELETE: api/cart/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCart(int id)
    {
      var _cart = await _context.Carts.FindAsync(id);

      if (_cart == null)
      {
        return NotFound();
      }

      _context.Carts.Remove(_cart);
      await _context.SaveChangesAsync();

      return Ok();
    }

    private bool CartExists(int id)
    {
      return _context.Carts.Any(e => e.CartId == id);
    }
  }
}
