using CarWebSite.DataAccess.Context;
using CarWebSite.Domain.Entities;
using CarWebSite.Domain.Models.CarImage;
using CarWebSite.Domain.Models.Responses;
using Microsoft.EntityFrameworkCore;

namespace CarWebSite.BusinessLayer.Core
{
    public class CarImageActions
    {
        protected CarImageActions() { }

        protected List<CarImageResponseDto> GetImagesByCarActionExecution(int carId)
        {
            var data = new List<CarImageResponseDto>();
            List<CarImage> images;

            using (var db = new AppDbContext())
            {
                images = db.CarImages
                    .Where(img => img.CarId == carId)
                    .ToList();
            }

            if (images.Count <= 0) return data;

            foreach (var item in images)
            {
                data.Add(new CarImageResponseDto
                {
                    Id = item.Id,
                    Url = item.Url,
                    IsCover = item.IsCover,
                    CarId = item.CarId
                });
            }

            return data;
        }

        protected ActionResponse AddImageActionExecution(CarImageCreateDto data)
        {
            if (string.IsNullOrWhiteSpace(data.Url))
            {
                return new ActionResponse
                {
                    IsSuccess = false,
                    Message = "Image URL is required."
                };
            }

            using (var db = new AppDbContext())
            {
                var entity = new CarImage
                {
                    Url = data.Url,
                    IsCover = data.IsCover,
                    CarId = data.CarId
                };

                db.CarImages.Add(entity);
                db.SaveChanges();
            }

            return new ActionResponse
            {
                IsSuccess = true,
                Message = "Image added successfully."
            };
        }

        protected ActionResponse DeleteImageActionExecution(int id)
        {
            using (var db = new AppDbContext())
            {
                var entity = db.CarImages.FirstOrDefault(img => img.Id == id);

                if (entity == null)
                {
                    return new ActionResponse
                    {
                        IsSuccess = false,
                        Message = "Image not found."
                    };
                }

                db.CarImages.Remove(entity);
                db.SaveChanges();
            }

            return new ActionResponse
            {
                IsSuccess = true,
                Message = "Image deleted successfully."
            };
        }

        protected int? GetCarOwnerActionExecution(int carId)
        {
            using (var db = new AppDbContext())
            {
                var announcement = db.Announcements.FirstOrDefault(a => a.CarId == carId);
                return announcement?.UserDataId;
            }
        }

        protected int? GetImageOwnerActionExecution(int imageId)
        {
            using (var db = new AppDbContext())
            {
                var image = db.CarImages
                    .Include(img => img.Car)
                        .ThenInclude(c => c.Announcement)
                    .FirstOrDefault(img => img.Id == imageId);
                return image?.Car?.Announcement?.UserDataId;
            }
        }
    }
}
