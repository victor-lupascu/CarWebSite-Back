using CarWebSite.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarWebSite.DataAccess.Context
{
    public class AppDbContext : DbContext
    {   
        //Core
        public AppDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
                optionsBuilder.UseSqlServer(DbSession.ConnectionString);
        }

        //Entities
        public DbSet<UserData> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<CarImage> CarImages { get; set; }
        public DbSet<FavoriteData> Favorites { get; set; }
        public DbSet<ContactMessageData> ContactMessages { get; set; }

    }
}