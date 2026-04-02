using CarWebSite.Domain.Entities;

namespace CarWebSite.BusinessLayer.Interfaces
{
    public interface IAnnouncementAction
    {
        Task<IEnumerable<Announcement>> GetAllAsync();
        Task<Announcement?> GetByIdAsync(int id);
        Task AddAsync(Announcement announcement);
        Task UpdateAsync(Announcement announcement);
        Task DeleteAsync(int id);
        Task<IEnumerable<Announcement>> GetAllWithDetailsAsync();
    }
}