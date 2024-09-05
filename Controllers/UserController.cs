using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Raw2PlateFuelPlusNetcore.Models;

namespace Raw2PlateFuelPlusNetcore.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UserController : ControllerBase
  {
    private readonly RawDBContext _context;
    public UserController(RawDBContext context)
    {
      _context = context;
    }

    // GET: api/user
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
      var _userList = await _context.Users.ToListAsync();

      return Ok(_userList);
    }

    // GET: api/user/1
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
      var _user = await _context.Users.FindAsync(id);

      if (_user == null)
      {
        return NotFound();
      }

      return Ok(_user);
    }

    // GET: api/user/email/johndoe@gmail.com/password/johndoe
    [HttpGet("email/{email}/password/{password}")]
    public async Task<ActionResult<User>> CheckUser(string email, string password)
    {
      var _user = await _context.Users
        .FirstOrDefaultAsync(
          user => user.Email == email && user.Password == password
        );

      if (_user == null)
      {
        return Unauthorized();
      }

      return Ok(_user);
    }

    // POST: api/user
    [HttpPost]
    public async Task<ActionResult<User>> PostUser(User _user)
    {
      // Check if email address is already in use
      var _existing = await _context.Users.FirstOrDefaultAsync(
        user => user.Email == _user.Email 
      );

      if (_existing != null)
      {
        return Conflict();
      }

      _context.Users.Add(_user);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetUser", new { id = _user.UserId }, _user);
    }

    // PUT: api/user/1
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(int id, User _user)
    {
      if (id != _user.UserId)
      {
        return BadRequest();
      }

      _context.Entry(_user).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!UserExists(id))
        {
          return Unauthorized();
        }
        else
        {
          return NotFound();
        }
      }

      return Ok(_user);
    }

    // DELETE: api/user/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
      var _user = await _context.Users.FindAsync(id);

      if (_user == null)
      {
        return NotFound();
      }

      _context.Users.Remove(_user);
      await _context.SaveChangesAsync();

      return Ok();
    }

    private bool UserExists(int id)
    {
      return _context.Users.Any(e => e.UserId == id);
    }
  }
}
