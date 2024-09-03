using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Raw2PlateFuelPlusNetcore.Models;

namespace Raw2PlateFuelPlusNetcore.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class OrderController : ControllerBase
  {
    private readonly RawDBContext _context;
    public OrderController(RawDBContext context)
    {
      _context = context;
    }

    // GET: api/order
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
    {
      var _orderList = await _context.Orders.ToListAsync();

      return Ok(_orderList);
    }

    // GET: api/order/1
    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrder(int id)
    {
      var _order = await _context.Orders.FindAsync(id);

      if (_order == null)
      {
        return NotFound();
      }

      return Ok(_order);
    }

    // POST: api/order
    [HttpPost]
    public async Task<ActionResult<Order>> PostOrder(Order _order)
    {
      _context.Orders.Add(_order);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetOrder", new { id = _order.OrderId }, _order);
    }

    // PUT: api/order/1
    [HttpPut]
    public async Task<IActionResult> PutOrder(int id, Order _order)
    {
      if (id != _order.OrderId)
      {
        return BadRequest();
      }

      _context.Entry(_order).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!OrderExists(id))
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

    // DELETE: api/order/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
      var _order = await _context.Orders.FindAsync(id);

      if (_order == null)
      {
        return NotFound();
      }

      _context.Orders.Remove(_order);
      await _context.SaveChangesAsync();

      return Ok();
    }

    private bool OrderExists(int id)
    {
      return _context.Orders.Any(e => e.OrderId == id);
    }
  }
}
