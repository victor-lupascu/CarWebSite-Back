using CarWebSite.DataAccess.Context;
using CarWebSite.DataAccess.Repositories.Interfaces;
using CarWebSite.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarWebSite.DataAccess.Repositories.Implementations
{
    public class FavoriteRepository : GenericRepository<FavoriteData>, IFavoriteRepository
    {
        public FavoriteRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<FavoriteData>> GetByUserIdAsync(int userId)
        {
            return await _context.Favorites
                .Where(f => f.UserDataId == userId)
                .Include(f => f.Car)
                    .ThenInclude(c => c.Brand)
                .Include(f => f.Car)
                    .ThenInclude(c => c.Images)
                .ToListAsync();
        }
    }
}