using CarWebSite.Domain.Entities;

namespace CarWebSite.DataAccess.Repositories.Interfaces
{
    public interface ICarRepository : IGenericRepository<Car>
    {
        Task<IEnumerable<Car>> GetAllWithDetailsAsync();
        Task<Car?> GetByIdWithDetailsAsync(int id);
    }
}
