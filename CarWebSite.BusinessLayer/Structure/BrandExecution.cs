using CarWebSite.BusinessLayer.Interfaces;
using CarWebSite.Domain.Entities;

namespace CarWebSite.BusinessLayer.Structure
{
    public class BrandExecution
    {
        private readonly IBrandAction _brandAction;

        public BrandExecution(IBrandAction brandAction)
        {
            _brandAction = brandAction;
        }

        public async Task<IEnumerable<Brand>> GetAllBrandsAsync() =>
            await _brandAction.GetAllAsync();

        public async Task<Brand?> GetBrandByIdAsync(int id) =>
            await _brandAction.GetByIdAsync(id);

        public async Task CreateBrandAsync(Brand brand) =>
            await _brandAction.AddAsync(brand);

        public async Task UpdateBrandAsync(Brand brand) =>
            await _brandAction.UpdateAsync(brand);

        public async Task DeleteBrandAsync(int id) =>
            await _brandAction.DeleteAsync(id);
    }
}