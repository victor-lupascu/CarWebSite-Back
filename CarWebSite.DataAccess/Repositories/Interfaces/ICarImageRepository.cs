using CarWebSite.Domain.Entities;

namespace CarWebSite.DataAccess.Repositories.Interfaces
{
    public interface ICarImageRepository : IGenericRepository<CarImage>
    {
        Task<IEnumerable<CarImage>> GetByCarIdAsync(int carId);
    }
}
