using CarWebSite.Domain.Entities;

namespace CarWebSite.DataAccess.Repositories.Interfaces
{
    public interface IContactMessageRepository : IGenericRepository<ContactMessageData>
    {
        Task<IEnumerable<ContactMessageData>> GetByEmailAsync(string email);
    }
}