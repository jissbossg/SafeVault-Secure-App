using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SafeVault.API.Data;
using SafeVault.API.Services;

namespace SafeVault.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // Solo el rol 'Admin' puede ver todos los usuarios
        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllUsers()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }

        // Búsqueda segura contra Inyección SQL (EF Core parametriza automáticamente)
        [HttpGet("search")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult SearchUser(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) return BadRequest("Búsqueda inválida.");

            // Uso de LINQ que previene SQLi
            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            if (user == null) return NotFound();

            // Sanitización del output contra XSS
            user.Username = InputSanitizer.Sanitize(user.Username);
            return Ok(user);
        }
    }
}
