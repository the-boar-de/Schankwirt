using Microsoft.EntityFrameworkCore;
using DiscordBot.Database;

namespace DiscordBot.Database
{   
    public class DataBaseLogs : DbContext
    {
        public DataBaseLogs(DbContextOptions options) : base(options) { }
    
        public DbSet<DiscordBot.Database.Logs> Users { get; set; }
    }
}