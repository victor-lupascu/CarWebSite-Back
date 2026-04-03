using CarWebSite.Domain.Entities;

namespace CarWebSite.DataAccess.Repositories.Interfaces
{
    public interface IAnnouncementRepository : IGenericRepository<Announcement>
    {
        Task<IEnumerable<Announcement>> GetAllWithDetailsAsync();
    }
}
