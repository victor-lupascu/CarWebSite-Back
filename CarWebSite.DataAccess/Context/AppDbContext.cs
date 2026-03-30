using CarWebSite.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarWebSite.DataAccess.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserData> Users { get; set; }

        //------ De adaugat cand colegul termina Domain-ul ---------//

        // public DbSet<Car> Cars { get; set; }
        // public DbSet<Brand> Brands { get; set; }
    }
}