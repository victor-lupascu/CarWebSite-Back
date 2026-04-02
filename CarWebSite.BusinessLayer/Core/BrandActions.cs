using CarWebSite.BusinessLayer.Interfaces;
using CarWebSite.DataAccess.Repositories.Interfaces;
using CarWebSite.Domain.Entities;

namespace CarWebSite.BusinessLayer.Core
{
    public class BrandActions : IBrandAction
    {
        private readonly IBrandRepository _repository;

        public BrandActions(IBrandRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Brand>> GetAllAsync() =>
            await _repository.GetAllAsync();

        public async Task<Brand?> GetByIdAsync(int id) =>
            await _repository.GetByIdAsync(id);

        public async Task AddAsync(Brand brand) =>
            await _repository.AddAsync(brand);

        public async Task UpdateAsync(Brand brand) =>
            await _repository.UpdateAsync(brand);

        public async Task DeleteAsync(int id) =>
            await _repository.DeleteAsync(id);
    }
}