using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music_Portal.Models;

namespace Music_Portal.Controllers
{
    [ApiController]
    [Route("api/Songs")]
    public class SongsController : ControllerBase
    {
        private readonly MusicPortalContext _context;

        public SongsController(MusicPortalContext context)
        {
            _context = context;
        }

        // GET: api/Songs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongs()
        {
            return await _context.Songs.ToListAsync();
        }

        // GET: api/Songs/3
        [HttpGet("{id}")]
        public async Task<ActionResult<Song>> GetSong(int id)
        {
            var song = await _context.Songs.SingleOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }
            return new ObjectResult(song);
        }

        // PUT: api/Songs
        [HttpPut]
        public async Task<ActionResult<Song>> PutSong(Song song)
        {
            if (string.IsNullOrEmpty(song.Title) || song.Title.Length > 200)
            {
                ModelState.AddModelError("Title", "Назва пісні не може бути пустою і не довшою за 200 символів");
            }

            if (string.IsNullOrEmpty(song.Artist) || song.Artist.Length > 100)
            {
                ModelState.AddModelError("Artist", "Ім'я виконавця не може бути пустим і не довшим за 100 символів");
            }

            if (song.GenreId < 1)
            {
                ModelState.AddModelError("GenreId", "Оберіть коректний жанр");
            }

            if (song.Duration < 1 || song.Duration > 3600)
            {
                ModelState.AddModelError("Duration", "Тривалість має бути від 1 до 3600 секунд");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_context.Songs.Any(e => e.Id == song.Id))
            {
                return NotFound();
            }

            _context.Update(song);
            await _context.SaveChangesAsync();
            return Ok(song);
        }

        // POST: api/Songs
        [HttpPost]
        public async Task<ActionResult<Song>> PostSong(Song song)
        {
            if (string.IsNullOrEmpty(song.Title) || song.Title.Length > 200)
            {
                ModelState.AddModelError("Title", "Назва пісні не може бути пустою і не довшою за 200 символів");
            }

            if (string.IsNullOrEmpty(song.Artist) || song.Artist.Length > 100)
            {
                ModelState.AddModelError("Artist", "Ім'я виконавця не може бути пустим і не довшим за 100 символів");
            }

            if (song.GenreId < 1)
            {
                ModelState.AddModelError("GenreId", "Оберіть коректний жанр");
            }

            if (song.Duration < 1 || song.Duration > 3600)
            {
                ModelState.AddModelError("Duration", "Тривалість має бути від 1 до 3600 секунд");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Songs.Add(song);
            await _context.SaveChangesAsync();

            return Ok(song);
        }

        // DELETE: api/Songs/3
        [HttpDelete("{id}")]
        public async Task<ActionResult<Song>> DeleteSong(int id)
        {
            var song = await _context.Songs.SingleOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }

            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();

            return Ok(song);
        }
    }
}