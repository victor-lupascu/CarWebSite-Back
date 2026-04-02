using CarWebSite.Domain.Entities;

namespace CarWebSite.DataAccess.Repositories.Interfaces
{
    public interface IFavoriteRepository : IGenericRepository<FavoriteData>
    {
        Task<IEnumerable<FavoriteData>> GetByUserIdAsync(int userId);
    }
}