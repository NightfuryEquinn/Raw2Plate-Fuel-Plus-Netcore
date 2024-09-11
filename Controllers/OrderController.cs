using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Raw2PlateFuelPlusNetcore.DTOs;
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

    // GET: api/order/user/1
    [HttpGet("user/{id}")]
    public async Task<ActionResult<Order>> GetOrderByUserId(int id)
    {
      // Get first active order only
      var _order = await (from order in _context.Orders
                          join orderItem in _context.OrderItems on order.OrderId equals orderItem.OrderId
                          join item in _context.Items on orderItem.ItemId equals item.ItemId
                          join store in _context.Stores on item.StoreId equals store.StoreId
                          where order.UserId == id && (order.Status == "Pending" || order.Status == "Delivering")
                          select new OrderInfoDTO
                          {
                            OrderId = order.OrderId,
                            Receiver = order.Receiver,
                            Contact = order.Contact,
                            Address = order.Address,
                            TotalPrice = order.TotalPrice,
                            PaidWith = order.PaidWith,
                            Status = order.Status,
                            Date = order.Date,
                            OrderTime = order.OrderTime,
                            DeliveredTime = order.DeliveredTime,
                            Driver = order.Driver,
                            UserId = order.UserId,
                            StoreId = store.StoreId,
                            StoreName = store.Name,
                            StoreImage = store.Image,
                          }).FirstOrDefaultAsync();

      if (_order == null)
      {
        return BadRequest();
      }

      return Ok(_order);
    }

    // GET: api/order/user/1/items
    [HttpGet("user/{id}/items")]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrderItem(int id)
    {
      var _items = await (from item in _context.Items
                          join orderItem in _context.OrderItems on item.ItemId equals orderItem.ItemId
                          where orderItem.OrderId == id
                          select new
                          {
                            item.ItemId,
                            item.Name,
                            orderItem.Quantity,
                            item.Price
                          }).ToListAsync();

      if (_items == null)
      {
        return BadRequest();
      }

      return Ok(_items);
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
    [HttpPut("{id}")]
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
          return Unauthorized();
        }
        else
        {
          return NotFound();
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
