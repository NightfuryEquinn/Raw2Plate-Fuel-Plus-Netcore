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

    // GET: api/bookmark/userId/1
    [HttpGet("userId/{id}")]
    public async Task<ActionResult<IEnumerable<Bookmark>>> GetBookmarkByUserId(int id)
    {
      var _bookmarkList = await _context.Bookmarks
        .Where(bookmark => bookmark.UserId == id)
        .ToListAsync();

      if (_bookmarkList == null)
      {
        return BadRequest();
      }

      return Ok(_bookmarkList);
    }

    // POST: api/bookmark
    [HttpPost]
    public async Task<ActionResult<Bookmark>> PostBookmark(Bookmark _bookmark)
    {
      // Check if recipe is already bookmarked
      var _existing = await _context.Bookmarks.FirstOrDefaultAsync(
        bookmark => bookmark.RecipeId == _bookmark.RecipeId
      );

      if (_existing != null)
      {
        return Conflict();
      }

      _context.Bookmarks.Add(_bookmark);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetBookmark", new { id = _bookmark.BookmarkId }, _bookmark);
    }

    // PUT: api/bookmark/1
    [HttpPut("{id}")]
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
          return Unauthorized();
        }
        else
        {
          return NotFound();
        }
      }

      return Ok();
    }

    // DELETE: api/bookmark/1/645383
    [HttpDelete("{userId}/{recipeId}")]
    public async Task<IActionResult> DeleteBookmark(int userId, int recipeId)
    {
      var _bookmark = await _context.Bookmarks
        .Where(bookmark => bookmark.UserId == userId && bookmark.RecipeId == recipeId)
        .FirstOrDefaultAsync();

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
