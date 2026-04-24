using CarWebSite.DataAccess.Context;
using CarWebSite.Domain.Entities;
using CarWebSite.Domain.Models.Car;
using CarWebSite.Domain.Models.Brand;
using CarWebSite.Domain.Models.CarImage;
using Microsoft.EntityFrameworkCore;

namespace CarWebSite.BusinessLayer.Core
{
    public class CarActions
    {
        protected CarActions() { }

        protected List<CarResponseDto> GetAllCarsActionExecution()
        {
            var data = new List<CarResponseDto>();
            List<Car> cars;

            using (var db = new AppDbContext())
            {
                cars = db.Cars
                    .Include(c => c.Brand)
                    .Include(c => c.Images)
                    .ToList();
            }

            if (cars.Count <= 0) return data;

            foreach (var item in cars)
            {
                data.Add(MapToDto(item));
            }

            return data;
        }

        protected CarResponseDto? GetCarByIdActionExecution(int id)
        {
            Car? entity;

            using (var db = new AppDbContext())
            {
                entity = db.Cars
                    .Include(c => c.Brand)
                    .Include(c => c.Images)
                    .FirstOrDefault(c => c.Id == id);
            }

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
