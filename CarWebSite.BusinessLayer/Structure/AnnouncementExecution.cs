using CarWebSite.BusinessLayer.Interfaces;
using CarWebSite.Domain.Entities;

namespace CarWebSite.BusinessLayer.Structure
{
    public class AnnouncementExecution
    {
        private readonly IAnnouncementAction _announcementAction;

        public AnnouncementExecution(IAnnouncementAction announcementAction)
        {
            _announcementAction = announcementAction;
        }

        public async Task<IEnumerable<Announcement>> GetAllAnnouncementsAsync() =>
            await _announcementAction.GetAllAsync();

        public async Task<IEnumerable<Announcement>> GetAllWithDetailsAsync() =>
            await _announcementAction.GetAllWithDetailsAsync();

        public async Task<Announcement?> GetAnnouncementByIdAsync(int id) =>
            await _announcementAction.GetByIdAsync(id);

        public async Task CreateAnnouncementAsync(Announcement announcement) =>
            await _announcementAction.AddAsync(announcement);

        public async Task UpdateAnnouncementAsync(Announcement announcement) =>
            await _announcementAction.UpdateAsync(announcement);

        public async Task DeleteAnnouncementAsync(int id) =>
            await _announcementAction.DeleteAsync(id);
    }
}