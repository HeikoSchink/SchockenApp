
using Microsoft.AspNetCore.Mvc;
using SchockenAppBackend.Models;
using SchockenAppBackend.Services;
using SchockenAppBackend.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SchockenAppBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtService _jwtService;

        public AuthController(ApplicationDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            if (_context.Users.Any(u => u.Username == user.Username))
                return BadRequest("User already exists");

            using var hmac = new HMACSHA256();
            user.PasswordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(user.PasswordHash)));

            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login(User login)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == login.Username);
            if (user == null) return Unauthorized();

            using var hmac = new HMACSHA256();
            var computedHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(login.PasswordHash)));

            if (user.PasswordHash != computedHash) return Unauthorized();

            var token = _jwtService.GenerateToken(user.Username);
            return Ok(new { token });
        }
    }
}
