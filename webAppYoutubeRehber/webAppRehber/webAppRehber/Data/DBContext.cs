using Microsoft.EntityFrameworkCore;
using webAppRehber.Models;

namespace webAppRehber.Data
{
    public class DBContext : DbContext
    {
            public DBContext(DbContextOptions<DBContext> options) : base(options)
            {
            }
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                if (!optionsBuilder.IsConfigured)
                {
                    var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
                    var configuration = builder.Build();

                    optionsBuilder.UseSqlServer(configuration.GetConnectionString("Default"));
                }
            }

        public DbSet<newRehber> newRehbers { get; set; }
        public DbSet<newLogin> newLogins { get; set; }

    }

    
}
