using CarWebSite.DataAccess.Context;
using CarWebSite.DataAccess.Repositories.Interfaces;
using CarWebSite.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarWebSite.DataAccess.Repositories.Implementations
{
    public class CarRepository : GenericRepository<Car>, ICarRepository
    {
        public CarRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Car>> GetAllWithDetailsAsync()
        {
            return await _context.Cars
                .Include(c => c.Brand)
                .Include(c => c.Images)
                .ToListAsync();
        }

        public async Task<Car?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Cars
                .Include(c => c.Brand)
                .Include(c => c.Images)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
