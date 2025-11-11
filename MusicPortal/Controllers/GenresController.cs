using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music_Portal.Models;

namespace Music_Portal.Controllers
{
    [ApiController]
    [Route("api/Genres")]
    public class GenresController : ControllerBase
    {
        private readonly MusicPortalContext _context;

        public GenresController(MusicPortalContext context)
        {
            _context = context;
        }

        // GET: api/Genres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
        {
            return await _context.Genres.ToListAsync();
        }

        // GET: api/Genres/3
        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> GetGenre(int id)
        {
            var genre = await _context.Genres.SingleOrDefaultAsync(m => m.Id == id);
            if (genre == null)
            {
                return NotFound();
            }
            return new ObjectResult(genre);
        }

        // PUT: api/Genres
        [HttpPut]
        public async Task<ActionResult<Genre>> PutGenre(Genre genre)
        {
            if (string.IsNullOrEmpty(genre.Name) || genre.Name.Length < 2 || genre.Name.Length > 100)
            {
                ModelState.AddModelError("Name", "Назва жанру має бути від 2 до 100 символів");
            }

            if (genre.Description != null && genre.Description.Length > 500)
            {
                ModelState.AddModelError("Description", "Опис не може бути довшим за 500 символів");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_context.Genres.Any(e => e.Id == genre.Id))
            {
                return NotFound();
            }

            _context.Update(genre);
            await _context.SaveChangesAsync();
            return Ok(genre);
        }

        // POST: api/Genres
        [HttpPost]
        public async Task<ActionResult<Genre>> PostGenre(Genre genre)
        {
            if (string.IsNullOrEmpty(genre.Name) || genre.Name.Length < 2 || genre.Name.Length > 100)
            {
                ModelState.AddModelError("Name", "Назва жанру має бути від 2 до 100 символів");
            }

            if (genre.Description != null && genre.Description.Length > 500)
            {
                ModelState.AddModelError("Description", "Опис не може бути довшим за 500 символів");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();

            return Ok(genre);
        }

        // DELETE: api/Genres/3
        [HttpDelete("{id}")]
        public async Task<ActionResult<Genre>> DeleteGenre(int id)
        {
            var genre = await _context.Genres.SingleOrDefaultAsync(m => m.Id == id);
            if (genre == null)
            {
                return NotFound();
            }

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();

            return Ok(genre);
        }
    }
}