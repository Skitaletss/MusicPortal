using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music_Portal.Models;

namespace Music_Portal.Controllers
{
    [ApiController]
    [Route("api/Users")]
    public class UsersController : ControllerBase
    {
        private readonly MusicPortalContext _context;

        public UsersController(MusicPortalContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/3
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return new ObjectResult(user);
        }

        // PUT: api/Users
        [HttpPut]
        public async Task<ActionResult<User>> PutUser(User user)
        {
            if (string.IsNullOrEmpty(user.Username) || user.Username.Length < 3 || user.Username.Length > 50)
            {
                ModelState.AddModelError("Username", "Ім'я користувача має бути від 3 до 50 символів");
            }

            if (string.IsNullOrEmpty(user.Email) || !user.Email.Contains("@"))
            {
                ModelState.AddModelError("Email", "Невірний формат email");
            }

            if (string.IsNullOrEmpty(user.Password) || user.Password.Length < 6)
            {
                ModelState.AddModelError("Password", "Пароль має містити принаймні 6 символів");
            }

            if (string.IsNullOrEmpty(user.Role) || (user.Role != "User" && user.Role != "Admin"))
            {
                ModelState.AddModelError("Role", "Роль має бути або 'User' або 'Admin'");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_context.Users.Any(e => e.Id == user.Id))
            {
                return NotFound();
            }

            _context.Update(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (string.IsNullOrEmpty(user.Username) || user.Username.Length < 3 || user.Username.Length > 50)
            {
                ModelState.AddModelError("Username", "Ім'я користувача має бути від 3 до 50 символів");
            }

            if (string.IsNullOrEmpty(user.Email) || !user.Email.Contains("@"))
            {
                ModelState.AddModelError("Email", "Невірний формат email");
            }

            if (string.IsNullOrEmpty(user.Password) || user.Password.Length < 6)
            {
                ModelState.AddModelError("Password", "Пароль має містити принаймні 6 символів");
            }

            if (string.IsNullOrEmpty(user.Role) || (user.Role != "User" && user.Role != "Admin"))
            {
                ModelState.AddModelError("Role", "Роль має бути або 'User' або 'Admin'");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        // DELETE: api/Users/3
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }
    }
}