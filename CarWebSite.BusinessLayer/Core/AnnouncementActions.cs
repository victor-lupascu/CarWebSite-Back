using CarWebSite.DataAccess.Repositories.Interfaces;
using CarWebSite.BusinessLayer.Interfaces;
using CarWebSite.Domain.Entities;
using CarWebSite.Domain.Models.Announcement;
using CarWebSite.Domain.Models.Brand;
using CarWebSite.Domain.Models.CarImage;
using CarWebSite.Domain.Models.Responses;

namespace CarWebSite.BusinessLayer.Core
{
    public class AnnouncementActions : IAnnouncementAction
    {
        private readonly IAnnouncementRepository _repository;

        public AnnouncementActions(IAnnouncementRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<AnnouncementResponseDto>> GetAllAnnouncementAction()
        {
            var entities = await _repository.GetAllWithDetailsAsync();
            return entities.Select(e => MapToDto(e)).ToList();
        }

        public async Task<AnnouncementResponseDto?> GetAnnouncementByIdAction(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;
            return MapToDto(entity);
        }

        public async Task<ActionResponse> CreateAnnouncementAction(AnnouncementCreateDto data)
        {
            if (string.IsNullOrWhiteSpace(data.Title))
            {
                return new ActionResponse
                {
                    IsSuccess = false,
                    Message = "Title is required."
                };
            }

            var car = new Car
            {
                Model = data.Model,
                Year = data.Year,
                Mileage = data.Mileage,
                Price = data.Price,
                FuelType = data.FuelType,
                Transmission = data.Transmission,
                Condition = data.Condition,
                Description = data.Description,
                BodyType = data.BodyType,
                Color = data.Color,
                Doors = data.Doors,
                Seats = data.Seats,
                EngineSize = data.EngineSize,
                Horsepower = data.Horsepower,
                VIN = data.VIN,
                BrandId = data.BrandId
            };

            var announcement = new Announcement
            {
                Title = data.Title,
                Negotiable = data.Negotiable,
                ShowPhone = data.ShowPhone,
                PublishedAt = DateTime.UtcNow,
                Car = car
            };

            await _repository.AddAsync(announcement);

            return new ActionResponse
            {
                IsSuccess = true,
                Message = "Announcement created successfully."
            };
        }

        public async Task<ActionResponse> UpdateAnnouncementAction(AnnouncementUpdateDto data)
        {
            return new ActionResponse
            {
                IsSuccess = false,
                Message = "Not implemented yet."
            };
        }

        public async Task<ActionResponse> DeleteAnnouncementAction(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                return new ActionResponse
                {
                    IsSuccess = false,
                    Message = "Announcement not found."
                };
            }

            await _repository.DeleteAsync(id);

            return new ActionResponse
            {
                IsSuccess = true,
                Message = "Announcement deleted successfully."
            };
        }

        private AnnouncementResponseDto MapToDto(Announcement entity)
        {
            return new AnnouncementResponseDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Status = entity.Status,
                Negotiable = entity.Negotiable,
                ShowPhone = entity.ShowPhone,
                Views = entity.Views,
                Inquiries = entity.Inquiries,
                PublishedAt = entity.PublishedAt,
                UserId = entity.UserDataId,
                OwnerName = entity.UserData?.FullName ?? "",
                OwnerPhone = entity.UserData?.PhoneNumber,
                CarId = entity.CarId,
                Model = entity.Car?.Model ?? "",
                Year = entity.Car?.Year ?? 0,
                Mileage = entity.Car?.Mileage ?? 0,
                Price = entity.Car?.Price ?? 0,
                FuelType = entity.Car?.FuelType ?? 0,
                Transmission = entity.Car?.Transmission ?? 0,
                Condition = entity.Car?.Condition ?? 0,
                Description = entity.Car?.Description ?? "",
                BodyType = entity.Car?.BodyType ?? 0,
                Color = entity.Car?.Color?.ToString(),
                Doors = entity.Car?.Doors.HasValue == true ? (int?)entity.Car.Doors.Value : null,   
                Seats = entity.Car?.Seats,
                EngineSize = entity.Car?.EngineSize,
                Horsepower = entity.Car?.Horsepower,
                VIN = entity.Car?.VIN,
                Brand = new BrandResponseDto
                {
                    Id = entity.Car?.Brand?.Id ?? 0,
                    Name = entity.Car?.Brand?.Name ?? ""
                },
                Images = entity.Car?.Images?.Select(img => new CarImageResponseDto
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

