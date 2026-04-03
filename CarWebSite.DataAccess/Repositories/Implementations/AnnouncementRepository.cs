using CarWebSite.DataAccess.Context;
using CarWebSite.DataAccess.Repositories.Interfaces;
using CarWebSite.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarWebSite.DataAccess.Repositories.Implementations
{
    public class AnnouncementRepository : GenericRepository<Announcement>, IAnnouncementRepository
    {
        public AnnouncementRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Announcement>> GetAllWithDetailsAsync()
        {
            return await _context.Announcements
                .Include(a => a.Car)
                    .ThenInclude(c => c.Brand)
                .Include(a => a.Car)
                    .ThenInclude(c => c.Images)
                .Include(a => a.UserData)
                .ToListAsync();
        }
    }
}