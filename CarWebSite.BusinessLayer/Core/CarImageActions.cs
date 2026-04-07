using CarWebSite.DataAccess.Repositories.Interfaces;
using CarWebSite.BusinessLayer.Interfaces;
using CarWebSite.Domain.Entities;
using CarWebSite.Domain.Models.CarImage;
using CarWebSite.Domain.Models.Responses;

namespace CarWebSite.BusinessLayer.Core
{
    public class CarImageActions : ICarImageAction
    {
        private readonly ICarImageRepository _repository;

        public CarImageActions(ICarImageRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CarImageResponseDto>> GetImagesByCarAction(int carId)
        {
            var entities = await _repository.GetByCarIdAsync(carId);
            return entities.Select(e => MapToDto(e)).ToList();
        }

        public async Task<ActionResponse> AddImageAction(CarImageCreateDto data)
        {
            if (string.IsNullOrWhiteSpace(data.Url))
            {
                return new ActionResponse
                {
                    IsSuccess = false,
                    Message = "Image URL is required."
                };
            }

            var entity = new CarImage
            {
                Url = data.Url,
                IsCover = data.IsCover,
                CarId = data.CarId
            };

            await _repository.AddAsync(entity);

            return new ActionResponse
            {
                IsSuccess = true,
                Message = "Image added successfully."
            };
        }

        public async Task<ActionResponse> DeleteImageAction(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                return new ActionResponse
                {
                    IsSuccess = false,
                    Message = "Image not found."
                };
            }

            await _repository.DeleteAsync(id);

            return new ActionResponse
            {
                IsSuccess = true,
                Message = "Image deleted successfully."
            };
        }

        private CarImageResponseDto MapToDto(CarImage entity)
        {
            return new CarImageResponseDto
            {
                Id = entity.Id,
                Url = entity.Url,
                IsCover = entity.IsCover,
                CarId = entity.CarId
            };
        }
    }
}
