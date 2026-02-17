using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Schankwirt.Database;

namespace Schankwirt.Database
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //-------------------------------------------------------------------------------------------
            //Database Configuration
            //General

            //Database host
            var mariadb_host = Environment.GetEnvironmentVariable("MARIADB_HOST") ?? "localhost";
            //Database Port 
            var mariadb_port = Environment.GetEnvironmentVariable("MARIADB_PORT") ?? "3306";
            //Database user 
            var mariadb_user = Environment.GetEnvironmentVariable("MARIADB_USER") ?? "testbot";
            //Database password 
            var mariadb_password = Environment.GetEnvironmentVariable("MARIADB_PASSWORD") ?? "testbot";

            //-------------------------------------------------------------------------------------------
            //Database - Logs

            //Data base Name 
            var mariadb_databaselogs = Environment.GetEnvironmentVariable("MARIADB_DATABASE") ?? "logs";

            // Connection String
            var connectionString = $"Server={mariadb_host};Port={mariadb_port};Database={mariadb_databaselogs};User={mariadb_user};Password={mariadb_password};";

            // Server Version
            var serverVersion = ServerVersion.AutoDetect(connectionString);
            
            // DbContext registrieren
            services.AddDbContext<DataBaseLogs>(options =>
                options.UseMySql(connectionString, serverVersion,
                 mySqlOptions => mySqlOptions.MigrationsAssembly("Schankwirt"))
            );
            //-------------------------------------------------------------------------------------------

            
            // Hier kommen später Discord Services rein
            // services.AddSingleton<DiscordSocketClient>();
        }
    }
}  
