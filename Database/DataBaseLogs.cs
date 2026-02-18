using Microsoft.EntityFrameworkCore;
using Schankwirt.Database;

namespace Schankwirt.Database
{   
    public class DataBaseLogs : DbContext
    {
        public DataBaseLogs(DbContextOptions options) : base(options) { }
    
        public DbSet<Logs> Logs { get; set; }

        public DbSet<WelcomeMessage> WelcomeMessage {get; set;}
    }
}