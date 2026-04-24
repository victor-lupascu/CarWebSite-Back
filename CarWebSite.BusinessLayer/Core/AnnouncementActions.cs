using CarWebSite.DataAccess.Context;
using CarWebSite.Domain.Entities;
using CarWebSite.Domain.Models.Announcement;
using CarWebSite.Domain.Models.Brand;
using CarWebSite.Domain.Models.CarImage;
using CarWebSite.Domain.Models.Responses;
using Microsoft.EntityFrameworkCore;

namespace CarWebSite.BusinessLayer.Core
{
    public class AnnouncementActions
    {
        protected AnnouncementActions() { }

        protected List<AnnouncementResponseDto> GetAllAnnouncementActionExecution()
        {
            var data = new List<AnnouncementResponseDto>();
            List<Announcement> announcements;

            using (var db = new AppDbContext())
            {
                announcements = db.Announcements
                    .Include(a => a.Car)
                        .ThenInclude(c => c.Brand)
                    .Include(a => a.Car)
                        .ThenInclude(c => c.Images)
                    .Include(a => a.UserData)
                    .ToList();
            }

            if (announcements.Count <= 0) return data;

            foreach (var item in announcements)
            {
                data.Add(MapToDto(item));
            }

            return data;
        }

        protected AnnouncementResponseDto? GetAnnouncementByIdActionExecution(int id)
        {
            Announcement? entity;

            using (var db = new AppDbContext())
            {
                entity = db.Announcements
                    .Include(a => a.Car)
                        .ThenInclude(c => c.Brand)
                    .Include(a => a.Car)
                        .ThenInclude(c => c.Images)
                    .Include(a => a.UserData)
                    .FirstOrDefault(a => a.Id == id);
            }

            if (entity == null) return null;
            return MapToDto(entity);
        }

        protected ActionResponse CreateAnnouncementActionExecution(AnnouncementCreateDto data)
        {
            if (string.IsNullOrWhiteSpace(data.Title))
            {
                return new ActionResponse
                {
                    IsSuccess = false,
                    Message = "Title is required."
                };
            }

            using (var db = new AppDbContext())
            {
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

                db.Announcements.Add(announcement);
                db.SaveChanges();
            }

            return new ActionResponse
            {
                IsSuccess = true,
                Message = "Announcement created successfully."
            };
        }

        protected ActionResponse UpdateAnnouncementActionExecution(AnnouncementUpdateDto data)
        {
            return new ActionResponse
            {
                IsSuccess = false,
                Message = "Not implemented yet."
            };
        }

        protected ActionResponse DeleteAnnouncementActionExecution(int id)
        {
            using (var db = new AppDbContext())
            {
                var entity = db.Announcements.FirstOrDefault(a => a.Id == id);

                if (entity == null)
                {
                    return new ActionResponse
                    {
                        IsSuccess = false,
                        Message = "Announcement not found."
                    };
                }

                db.Announcements.Remove(entity);
                db.SaveChanges();
            }

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
