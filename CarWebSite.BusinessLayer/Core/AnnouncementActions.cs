using CarWebSite.DataAccess.Context;
using CarWebSite.Domain.Entities;
using CarWebSite.Domain.Models.Announcement;
using CarWebSite.Domain.Models.Brand;
using CarWebSite.Domain.Models.CarImage;
using CarWebSite.Domain.Models.Responses;
using Microsoft.EntityFrameworkCore;
using CarWebSite.Domain.Enums;

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

        protected ActionResponse CreateAnnouncementActionExecution(AnnouncementCreateDto data, int userId)
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
                    UserDataId = userId,
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

        protected ActionResponse UpdateAnnouncementActionExecution(int id, AnnouncementUpdateDto data, int userId, bool isAsmin)
        {
            using (var db = new AppDbContext())
            {

                var announcement = db.Announcements
                    .Include(a => a.Car)
                    .FirstOrDefault(a => a.Id == id);

                if (announcement == null)
                {
                    return new ActionResponse
                    {
                        IsSuccess = false,
                        Message = "Announcement not found."
                    };
                }

                if(announcement.UserDataId != userId && !isAsmin)
                {
                    return new ActionResponse
                    {
                        IsSuccess = false,
                        Message = "Operation failed."
                    };
                }

                // Partial update: only non-null fields are applied; null means "leave unchanged".
                // Announcement fields
                if (data.Title != null) announcement.Title = data.Title;
                if(data.Negotiable != null) announcement.Negotiable = data.Negotiable.Value;
                if(data.ShowPhone != null) announcement.ShowPhone = data.ShowPhone.Value;
                if(data.Status != null) announcement.Status = data.Status.Value;

                // Car fields
                if(announcement.Car != null)
                {
                    if (data.Model != null) announcement.Car.Model = data.Model;
                    if (data.Year != null) announcement.Car.Year = data.Year.Value;
                    if (data.Mileage != null) announcement.Car.Mileage = data.Mileage.Value;
                    if (data.Price != null) announcement.Car.Price = data.Price.Value;
                    if (data.FuelType != null) announcement.Car.FuelType = data.FuelType.Value;
                    if (data.Transmission != null) announcement.Car.Transmission = data.Transmission.Value;
                    if (data.Condition != null) announcement.Car.Condition = data.Condition.Value;
                    if (data.Description != null) announcement.Car.Description = data.Description;
                    if (data.BodyType != null) announcement.Car.BodyType = data.BodyType.Value;
                    if (data.Color != null) announcement.Car.Color = data.Color.Value ;
                    if (data.Doors != null) announcement.Car.Doors = data.Doors.Value;
                    if (data.Seats != null) announcement.Car.Seats = data.Seats.Value;
                    if (data.EngineSize != null) announcement.Car.EngineSize = data.EngineSize;
                    if (data.Horsepower != null) announcement.Car.Horsepower = data.Horsepower.Value;
                    if (data.VIN != null) announcement.Car.VIN = data.VIN;
                    // Relinks the car to another brand by the foreign key(BrandId)
                    if (data.BrandId != null) announcement.Car.BrandId = data.BrandId.Value;
                }
                db.SaveChanges();
            }
            return new ActionResponse
            {
                IsSuccess = true,
                Message = "Announcement updated successfully." 

            };
        }

        protected ActionResponse DeleteAnnouncementActionExecution(int id, int userId, bool isAdmin)
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

                // Owner check
                if (entity.UserDataId != userId && !isAdmin)
                {
                    return new ActionResponse
                    {
                        IsSuccess = false,
                        Message = "Operation failed"
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
            // Maps entity to response DTO; uses safe defaults when a related
            // entity (Car/Brand/Images) is null, so non-nullable fields never receive null.
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
                OwnerName = entity.UserData != null ? entity.UserData.FullName : "",
                OwnerPhone = entity.UserData != null ? entity.UserData.PhoneNumber : null,
                CarId = entity.CarId,
                Model = entity.Car != null && entity.Car.Model != null ? entity.Car.Model : "",
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
                    Id = entity.Car != null && entity.Car.Brand != null ? entity.Car.Brand.Id : 0,
                    Name = entity.Car != null && entity.Car.Brand != null ? entity.Car.Brand.Name : ""
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
