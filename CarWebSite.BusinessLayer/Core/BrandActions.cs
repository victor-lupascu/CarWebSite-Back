using CarWebSite.DataAccess.Repositories.Interfaces;
using CarWebSite.BusinessLayer.Interfaces;
using CarWebSite.Domain.Entities;
using CarWebSite.Domain.Models.Brand;
using CarWebSite.Domain.Models.Responses;

namespace CarWebSite.BusinessLayer.Core
{
    public class BrandActions : IBrandAction
    {
        private readonly IBrandRepository _repository;

        public BrandActions(IBrandRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<BrandResponseDto>> GetAllBrandsAction()
        {
            var entities = await _repository.GetAllAsync();
            return entities.Select(e => MapToDto(e)).ToList();
        }

        public async Task<BrandResponseDto?> GetBrandByIdAction(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;
            return MapToDto(entity);
        }

        public async Task<ActionResponse> CreateBrandAction(BrandCreateDto data)
        {
            if (string.IsNullOrWhiteSpace(data.Name))
            {
                return new ActionResponse
                {
                    IsSuccess = false,
                    Message = "Brand name is required."
                };
            }

            var allBrands = await _repository.GetAllAsync();
            if (allBrands.Any(b => b.Name.ToLower() == data.Name.ToLower()))
            {
                return new ActionResponse
                {
                    IsSuccess = false,
                    Message = "Brand already exists."
                };
            }

            var entity = new Brand
            {
                Name = data.Name
            };

            await _repository.AddAsync(entity);

            return new ActionResponse
            {
                IsSuccess = true,
                Message = "Brand created successfully."
            };
        }

        public async Task<ActionResponse> DeleteBrandAction(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                return new ActionResponse
                {
                    IsSuccess = false,
                    Message = "Brand not found."
                };
            }

            await _repository.DeleteAsync(id);

            return new ActionResponse
            {
                IsSuccess = true,
                Message = "Brand deleted successfully."
            };
        }

        private BrandResponseDto MapToDto(Brand entity)
        {
            return new BrandResponseDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}
