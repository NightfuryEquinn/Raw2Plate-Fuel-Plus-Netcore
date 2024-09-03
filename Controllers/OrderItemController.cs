using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Raw2PlateFuelPlusNetcore.Models;

namespace Raw2PlateFuelPlusNetcore.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class OrderItemController : ControllerBase
  {
    private readonly RawDBContext _context;
    public OrderItemController(RawDBContext context)
    {
      _context = context;
    }

    // GET: api/orderitem
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderItem>>> GetOrderItems()
    {
      var _orderitemList = await _context.OrderItems.ToListAsync();

      return Ok(_orderitemList);
    }

    // GET: api/orderitem/1
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderItem>> GetOrderItem(int id)
    {
      var _orderitem = await _context.OrderItems.FindAsync(id);

      if (_orderitem == null)
      {
        return NotFound();
      }

      return Ok(_orderitem);
    }

    // POST: api/orderitem
    [HttpPost]
    public async Task<ActionResult<OrderItem>> PostOrderItem(OrderItem _orderitem)
    {
      _context.OrderItems.Add(_orderitem);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetOrderItem", new { id = _orderitem.OrderItemId }, _orderitem);
    }

    // PUT: api/orderitem/1
    [HttpPut]
    public async Task<IActionResult> PutOrderItem(int id, OrderItem _orderitem)
    {
      if (id != _orderitem.OrderItemId)
      {
        return BadRequest();
      }

      _context.Entry(_orderitem).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!OrderItemExists(id))
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

    // DELETE: api/orderitem/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrderItem(int id)
    {
      var _orderitem = await _context.OrderItems.FindAsync(id);

      if (_orderitem == null)
      {
        return NotFound();
      }

      _context.OrderItems.Remove(_orderitem);
      await _context.SaveChangesAsync();

      return Ok();
    }

    private bool OrderItemExists(int id)
    {
      return _context.OrderItems.Any(e => e.OrderItemId == id);
    }
  }
}
