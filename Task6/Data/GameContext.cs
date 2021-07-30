using Microsoft.EntityFrameworkCore;
using Task6.Models;

namespace Task6.Data
{
    public class GameContext : DbContext
    {
        public GameContext(DbContextOptions<GameContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Game> Games { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
