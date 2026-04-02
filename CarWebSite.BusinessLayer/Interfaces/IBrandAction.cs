using CarWebSite.Domain.Entities;

namespace CarWebSite.BusinessLayer.Interfaces
{
    public interface IBrandAction
    {
        Task<IEnumerable<Brand>> GetAllAsync();
        Task<Brand?> GetByIdAsync(int id);
        Task AddAsync(Brand brand);
        Task UpdateAsync(Brand brand);
        Task DeleteAsync(int id);
    }
}