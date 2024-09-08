using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Raw2PlateFuelPlusNetcore.DTOs;
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

    // GET: api/cart/user/1/store/1
    [HttpGet("user/{userId}/store/{storeId}")]
    public async Task<ActionResult<IEnumerable<Cart>>> GetCartByUserId(int userId, int storeId)
    {
      var _cartList = await (from cart in _context.Carts
                             join item in _context.Items on cart.ItemId equals item.ItemId
                             where cart.UserId == userId && item.StoreId == storeId
                             select new CartItemDTO 
                             { 
                               CartId = cart.CartId,
                               Quantity = cart.Quantity,
                               UserId = cart.UserId,
                               ItemId = cart.ItemId,
                               Name = item.Name,
                               Category = item.Category,
                               Image = item.Image,
                               Price = item.Price,
                               Description = item.Description,
                               StoreId = item.StoreId
                             }).ToListAsync();

      if (_cartList == null)
      {
        return BadRequest();
      }

      return Ok(_cartList);
    }

    // POST: api/cart
    [HttpPost]
    public async Task<ActionResult<Cart>> PostCart(Cart _cart)
    {
      _context.Carts.Add(_cart);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetCart", new { id = _cart.CartId }, _cart);
    }

    // PUT: api/cart/1
    [HttpPut("{id}")]
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
          return Unauthorized();
        }
        else
        {
          return NotFound();
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
