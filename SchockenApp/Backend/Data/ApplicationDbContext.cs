
using Microsoft.EntityFrameworkCore;
using SchockenAppBackend.Models;

namespace SchockenAppBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Lobby> Lobbies { get; set; }
        public DbSet<LobbyPlayer> LobbyPlayers { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<DiceRoll> DiceRolls { get; set; }
    }
}
