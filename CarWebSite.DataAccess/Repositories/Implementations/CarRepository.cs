using CarWebSite.DataAccess.Context;
using CarWebSite.DataAccess.Repositories.Interfaces;
using CarWebSite.Domain.Entities;

namespace CarWebSite.DataAccess.Repositories.Implementations
{
    public class CarRepository : GenericRepository<Car>, ICarRepository
    {
        public CarRepository(AppDbContext context) : base(context) { }
    }
}
