using CarWebSite.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarWebSite.DataAccess.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserData> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<CarImage> CarImages { get; set; }
    }
}