using CarWebSite.DataAccess.Context;
using CarWebSite.DataAccess.Repositories.Interfaces;
using CarWebSite.Domain.Entities;

namespace CarWebSite.DataAccess.Repositories.Implementations
{
    public class AnnouncementRepository : GenericRepository<Announcement>, IAnnouncementRepository
    {
        public AnnouncementRepository(AppDbContext context) : base(context) { }
    }
}
