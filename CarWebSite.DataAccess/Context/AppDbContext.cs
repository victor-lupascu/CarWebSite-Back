using CarWebSite.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarWebSite.DataAccess.Context
{
    public class AppDbContext : DbContext
    {   
        // Core
        public AppDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
                optionsBuilder.UseSqlServer(DbSession.ConnectionString);
        }

        // Entities
        public DbSet<UserData> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<CarImage> CarImages { get; set; }
        public DbSet<FavoriteData> Favorites { get; set; }
        public DbSet<ContactMessageData> ContactMessages { get; set; }

        // Relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Brand>().HasData(
                new Brand { Id = 1,  Name = "Audi" },
                new Brand { Id = 2,  Name = "BMW" },
                new Brand { Id = 3,  Name = "Chevrolet" },
                new Brand { Id = 4,  Name = "Dacia" },
                new Brand { Id = 5,  Name = "Ferrari" },
                new Brand { Id = 6,  Name = "Ford" },
                new Brand { Id = 7,  Name = "Honda" },
                new Brand { Id = 8,  Name = "Hyundai" },
                new Brand { Id = 9,  Name = "Kia" },
                new Brand { Id = 10, Name = "Lamborghini" },
                new Brand { Id = 11, Name = "Land Rover" },
                new Brand { Id = 12, Name = "Mazda" },
                new Brand { Id = 13, Name = "Mercedes-Benz" },
                new Brand { Id = 14, Name = "Nissan" },
                new Brand { Id = 15, Name = "Opel" },
                new Brand { Id = 16, Name = "Peugeot" },
                new Brand { Id = 17, Name = "Porsche" },
                new Brand { Id = 18, Name = "Renault" },
                new Brand { Id = 19, Name = "Seat" },
                new Brand { Id = 20, Name = "Skoda" },
                new Brand { Id = 21, Name = "Subaru" },
                new Brand { Id = 22, Name = "Tesla" },
                new Brand { Id = 23, Name = "Toyota" },
                new Brand { Id = 24, Name = "Volkswagen" },
                new Brand { Id = 25, Name = "Volvo" }
            );

            // Car-Announcement: 1:1 
            // Announcement is the dependent entity (holds the FK).
            // Deleting a Car cascades the deletion to its Announcement.
            modelBuilder.Entity<Car>()
                .HasOne(c => c.Announcement)
                .WithOne(a => a.Car)
                .HasForeignKey<Announcement>(a => a.CarId)
                .OnDelete(DeleteBehavior.Cascade);


            // Brand-Cars: 1:N
            // Brands are stable references.Deleting a Brand is blocked
            // while Cars reference it,preventing accidental loss.
            modelBuilder.Entity<Brand>()
                .HasMany(b => b.Cars)
                .WithOne(c => c.Brand)
                .HasForeignKey(c => c.BrandId)
                .OnDelete(DeleteBehavior.Restrict);


            // Car - CarImages: 1:N
            // Images belong to a Car. Deleting the Car removes its images automatically,
            // avoiding orphaned image records.
            modelBuilder.Entity<Car>()
                .HasMany(c => c.Images)
                .WithOne(i => i.Car)
                .HasForeignKey(i => i.CarId)
                .OnDelete(DeleteBehavior.Cascade);


            // UserData - Announcements: 1:N
            // Ussers cannot be deleted while they have active announcements,
            // preventing silent removal of cars listed for sale.
            modelBuilder.Entity<UserData>()
                .HasMany(u => u.Announcements)
                .WithOne(a => a.UserData)
                .HasForeignKey(a => a.UserDataId)
                .OnDelete(DeleteBehavior.Restrict);


            // UseData - Favorites: 1:N
            // Favorites are private, user-specific data.
            // They are deleted alongside the user since no one else has access to them.
            modelBuilder.Entity<UserData>()
                .HasMany(u => u.Favorites)
                .WithOne(f => f.UserData)
                .HasForeignKey(f => f.UserDataId)
                .OnDelete(DeleteBehavior.Cascade);


            // Car - Favorites: 1:N
            // Restrict prevents the "multiple cascade paths" conflict with UserData - Favorites,
            // requiring explicit cleanup in Core before deleting a Car.
            modelBuilder.Entity<Car>()
                .HasMany<FavoriteData>()
                .WithOne(f => f.Car)
                .HasForeignKey(f => f.CarId)
                .OnDelete(DeleteBehavior.Restrict);


        }

    }
}