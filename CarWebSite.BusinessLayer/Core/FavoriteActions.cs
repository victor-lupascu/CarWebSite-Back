using CarWebSite.DataAccess.Repositories.Interfaces;
using CarWebSite.BusinessLayer.Interfaces;
using CarWebSite.Domain.Entities;
using CarWebSite.Domain.Models.Favorite;
using CarWebSite.Domain.Models.Car;
using CarWebSite.Domain.Models.Brand;
using CarWebSite.Domain.Models.CarImage;
using CarWebSite.Domain.Models.Responses;

namespace CarWebSite.BusinessLayer.Core
{
    public class FavoriteActions : IFavoriteAction
    {
        private readonly IFavoriteRepository _repository;

        public FavoriteActions(IFavoriteRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<FavoriteResponseDto>> GetUserFavoritesAction(int userId)
        {
            var entities = await _repository.GetByUserIdAsync(userId);
            return entities.Select(e => MapToDto(e)).ToList();
        }

        public async Task<ActionResponse> AddFavoriteAction(int carId, int userId)
        {
            var existing = await _repository.GetByUserAndCarAsync(userId, carId);
            if (existing != null)
            {
                return new ActionResponse
                {
                    IsSuccess = false,
                    Message = "Car already in favorites."
                };
            }

            var favorite = new FavoriteData
            {
                UserDataId = userId,
                CarId = carId,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(favorite);

            return new ActionResponse
            {
                IsSuccess = true,
                Message = "Added to favorites."
            };
        }

        public async Task<ActionResponse> RemoveFavoriteAction(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                return new ActionResponse
                {
                    IsSuccess = false,
                    Message = "Favorite not found."
                };
            }

            await _repository.DeleteAsync(id);

            return new ActionResponse
            {
                IsSuccess = true,
                Message = "Removed from favorites."
            };
        }

        private FavoriteResponseDto MapToDto(FavoriteData entity)
        {
            return new FavoriteResponseDto
            {
                Id = entity.Id,
                CarId = entity.CarId,
                CreatedAt = entity.CreatedAt,
                Car = entity.Car != null ? new CarResponseDto
                {
                    Id = entity.Car.Id,
                    Model = entity.Car.Model ?? "",
                    Year = entity.Car.Year,
                    Mileage = entity.Car.Mileage,
                    Price = entity.Car.Price,
                    FuelType = entity.Car.FuelType,
                    Transmission = entity.Car.Transmission,
                    Condition = entity.Car.Condition,
                    Description = entity.Car.Description ?? "",
                    BodyType = entity.Car.BodyType,
                    Color = entity.Car.Color?.ToString(),
                    Doors = entity.Car.Doors.HasValue ? (int?)entity.Car.Doors.Value : null,
                    Seats = entity.Car.Seats,
                    EngineSize = entity.Car.EngineSize,
                    Horsepower = entity.Car.Horsepower,
                    VIN = entity.Car.VIN,
                    Brand = entity.Car.Brand != null ? new BrandResponseDto
                    {
                        Id = entity.Car.Brand.Id,
                        Name = entity.Car.Brand.Name ?? ""
                    } : new BrandResponseDto(),
                    Images = entity.Car.Images?.Select(img => new CarImageResponseDto
                    {
                        Id = img.Id,
                        Url = img.Url,
                        IsCover = img.IsCover,
                        CarId = img.CarId
                    }).ToList() ?? new List<CarImageResponseDto>()
                } : new CarResponseDto()
            };
        }
    }
}



