using CarWebSite.DataAccess.Context;
using CarWebSite.Domain.Entities;
using CarWebSite.Domain.Models.Favorite;
using CarWebSite.Domain.Models.Car;
using CarWebSite.Domain.Models.Brand;
using CarWebSite.Domain.Models.CarImage;
using CarWebSite.Domain.Models.Responses;
using Microsoft.EntityFrameworkCore;

namespace CarWebSite.BusinessLayer.Core
{
    public class FavoriteActions
    {
        protected FavoriteActions() { }

        protected List<FavoriteResponseDto> GetUserFavoritesActionExecution(int userId)
        {
            var data = new List<FavoriteResponseDto>();
            List<FavoriteData> favorites;

            using (var db = new AppDbContext())
            {
                favorites = db.Favorites
                    .Where(f => f.UserDataId == userId)
                    .Include(f => f.Car)
                        .ThenInclude(c => c.Brand)
                    .Include(f => f.Car)
                        .ThenInclude(c => c.Images)
                    .ToList();
            }

            if (favorites.Count <= 0) return data;

            foreach (var item in favorites)
            {
                data.Add(MapToDto(item));
            }

            return data;
        }

        protected ActionResponse AddFavoriteActionExecution(int carId, int userId)
        {
            if (carId <= 0 || userId <= 0)
            {
                return new ActionResponse
                {
                    IsSuccess = false,
                    Message = "Invalid parameters."
                };
            }

            using (var db = new AppDbContext())
            {
                var existing = db.Favorites
                    .FirstOrDefault(f => f.UserDataId == userId && f.CarId == carId);

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

                db.Favorites.Add(favorite);
                db.SaveChanges();
            }

            return new ActionResponse
            {
                IsSuccess = true,
                Message = "Added to favorites."
            };
        }

        protected ActionResponse RemoveFavoriteActionExecution(int id, int userId)
        {
            using (var db = new AppDbContext())
            {
                var entity = db.Favorites.FirstOrDefault(f => f.Id == id);

                if (entity == null)
                {
                    return new ActionResponse
                    {
                        IsSuccess = false,
                        Message = "Favorite not found."
                    };
                }

                // Owner check
                if(entity.UserDataId != userId)
                {
                    return new ActionResponse
                    {
                        IsSuccess = false,
                        Message = "Invalid operation"
                    };
                }

                db.Favorites.Remove(entity);
                db.SaveChanges();
            }

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