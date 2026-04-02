using CarWebSite.BusinessLayer.Interfaces;
using CarWebSite.DataAccess.Repositories.Interfaces;
using CarWebSite.Domain.Entities;

namespace CarWebSite.BusinessLayer.Core
{
    public class AnnouncementActions : IAnnouncementAction
    {
        private readonly IAnnouncementRepository _repository;

        public AnnouncementActions(IAnnouncementRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Announcement>> GetAllAsync() =>
            await _repository.GetAllAsync();

        public async Task<Announcement?> GetByIdAsync(int id) =>
            await _repository.GetByIdAsync(id);

        public async Task AddAsync(Announcement announcement) =>
            await _repository.AddAsync(announcement);

        public async Task UpdateAsync(Announcement announcement) =>
            await _repository.UpdateAsync(announcement);

        public async Task DeleteAsync(int id) =>
            await _repository.DeleteAsync(id);

        public async Task<IEnumerable<Announcement>> GetAllWithDetailsAsync() =>
            await _repository.GetAllAsync();
    }
}