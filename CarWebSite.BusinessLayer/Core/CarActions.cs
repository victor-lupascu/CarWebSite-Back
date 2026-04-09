using CarWebSite.DataAccess.Repositories.Interfaces;
using CarWebSite.BusinessLayer.Interfaces;
using CarWebSite.Domain.Entities;
using CarWebSite.Domain.Models.Car;
using CarWebSite.Domain.Models.Brand;
using CarWebSite.Domain.Models.CarImage;

namespace CarWebSite.BusinessLayer.Core
{
    public class CarActions : ICarAction
    {
        private readonly ICarRepository _repository;

        public CarActions(ICarRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CarResponseDto>> GetAllCarsAction()
        {
            var entities = await _repository.GetAllWithDetailsAsync();
            return entities.Select(e => MapToDto(e)).ToList();
        }

        public async Task<CarResponseDto?> GetCarByIdAction(int id)
        {
            var entity = await _repository.GetByIdWithDetailsAsync(id);
            if (entity == null) return null;
            return MapToDto(entity);
        }

        private CarResponseDto MapToDto(Car entity)
        {
            return new CarResponseDto
            {
                Id = entity.Id,
                Model = entity.Model,
                Year = entity.Year,
                Mileage = entity.Mileage,
                Price = entity.Price,
                FuelType = entity.FuelType,
                Transmission = entity.Transmission,
                Condition = entity.Condition,
                Description = entity.Description ?? "",
                BodyType = entity.BodyType,
                Color = entity.Color?.ToString(),
                Doors = entity.Doors.HasValue ? (int?)entity.Doors.Value : null,
                Seats = entity.Seats,
                EngineSize = entity.EngineSize,
                Horsepower = entity.Horsepower,
                VIN = entity.VIN,
                Brand = entity.Brand != null ? new BrandResponseDto
                {
                    Id = entity.Brand.Id,
                    Name = entity.Brand.Name ?? ""
                } : new BrandResponseDto(),
                Images = entity.Images?.Select(img => new CarImageResponseDto
                {
                    Id = img.Id,
                    Url = img.Url,
                    IsCover = img.IsCover,
                    CarId = img.CarId
                }).ToList() ?? new List<CarImageResponseDto>()
            };
        }
    }
}
