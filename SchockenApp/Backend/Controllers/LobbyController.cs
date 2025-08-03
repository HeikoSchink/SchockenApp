
using Microsoft.AspNetCore.Mvc;
using SchockenAppBackend.Data;
using SchockenAppBackend.Models;

namespace SchockenAppBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LobbyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LobbyController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetLobbies()
        {
            return Ok(_context.Lobbies.ToList());
        }

        [HttpPost("join")]
        public IActionResult JoinLobby(int lobbyId, int userId)
        {
            var lobbyPlayer = new LobbyPlayer { LobbyId = lobbyId, UserId = userId };
            _context.LobbyPlayers.Add(lobbyPlayer);
            _context.SaveChanges();
            return Ok();
        }
    }
}
