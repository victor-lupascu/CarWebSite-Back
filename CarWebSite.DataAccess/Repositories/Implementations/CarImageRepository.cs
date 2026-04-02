using CarWebSite.DataAccess.Context;
using CarWebSite.DataAccess.Repositories.Interfaces;
using CarWebSite.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarWebSite.DataAccess.Repositories.Implementations
{
    public class CarImageRepository : GenericRepository<CarImage>, ICarImageRepository
    {
        public CarImageRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<CarImage>> GetByCarIdAsync(int carId)
        {
            return await _context.CarImages
                .Where(ci => ci.CarId == carId)
                .ToListAsync();
        }
    }
}