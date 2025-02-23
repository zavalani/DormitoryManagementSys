using FindMeARoommate.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FindMeARoommate
{
    public class RoommateDbContext : DbContext
    {
        public RoommateDbContext()
        {
        }
        public RoommateDbContext(DbContextOptions<RoommateDbContext> options) :
        base(options)
        { }
        public DbSet<Students> Students { get; set; }
        public DbSet<Dormitories> Dormitories { get; set; }
        public DbSet<Applications> Applications { get; set; }
        public DbSet<Announcements> Announcements { get; set; }
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
