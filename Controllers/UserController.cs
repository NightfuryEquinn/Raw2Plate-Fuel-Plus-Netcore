﻿using Microsoft.AspNetCore.Http;
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

    // POST: api/user/1
    [HttpPost]
    public async Task<ActionResult<User>> PostUser(User _user)
    {
      _context.Users.Add(_user);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetUser", new { id = _user.UserId }, _user);
    }

    // PUT: api/user/1
    [HttpPut]
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
          return NotFound();
        }
        else
        {
          return BadRequest();
        }
      }

      return Ok();
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