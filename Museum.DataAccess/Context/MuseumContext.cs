using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Museum.DataAccess.Entities;

namespace Museum.DataAccess.Context
{
    public class MuseumContext : DbContext
    {
        public MuseumContext(DbContextOptions<MuseumContext> options) : base(options) { }
        public DbSet<Artwork> Artworks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
