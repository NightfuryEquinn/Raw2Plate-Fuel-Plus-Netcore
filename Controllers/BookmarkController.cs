using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Raw2PlateFuelPlusNetcore.Models;

namespace Raw2PlateFuelPlusNetcore.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class BookmarkController : ControllerBase
  {
    private readonly RawDBContext _context;
    public BookmarkController(RawDBContext context)
    {
      _context = context;
    }

    // GET: api/bookmark
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Bookmark>>> GetBookmarks()
    {
      var _bookmarkList = await _context.Bookmarks.ToListAsync();

      return Ok(_bookmarkList);
    }

    // GET: api/bookmark/1
    [HttpGet("{id}")]
    public async Task<ActionResult<Bookmark>> GetBookmark(int id)
    {
      var _bookmark = await _context.Bookmarks.FindAsync(id);

      if (_bookmark == null)
      {
        return NotFound();
      }

      return Ok(_bookmark);
    }

    // POST: api/bookmark
    [HttpPost]
    public async Task<ActionResult<Bookmark>> PostBookmark(Bookmark _bookmark)
    {
      _context.Bookmarks.Add(_bookmark);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetBookmark", new { id = _bookmark.BookmarkId }, _bookmark);
    }

    // PUT: api/bookmark/1
    [HttpPut]
    public async Task<IActionResult> PutBookmark(int id, Bookmark _bookmark)
    {
      if (id != _bookmark.BookmarkId)
      {
        return BadRequest();
      }

      _context.Entry(_bookmark).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!BookmarkExists(id))
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

    // DELETE: api/bookmark/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBookmark(int id)
    {
      var _bookmark = await _context.Bookmarks.FindAsync(id);

      if (_bookmark == null)
      {
        return NotFound();
      }

      _context.Bookmarks.Remove(_bookmark);
      await _context.SaveChangesAsync();

      return Ok();
    }

    private bool BookmarkExists(int id)
    {
      return _context.Bookmarks.Any(e => e.BookmarkId == id);
    }
  }
}
