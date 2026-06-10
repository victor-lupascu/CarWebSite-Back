using CarWebSite.DataAccess.Context;
using CarWebSite.Domain.Entities;
using CarWebSite.Domain.Models.CarImage;
using CarWebSite.Domain.Models.Responses;

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

        protected ActionResponse AddImageActionExecution(CarImageCreateDto data, int userId)
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
                var announcement = db.Announcements.FirstOrDefault(a => a.CarId == data.CarId);
                if (announcement == null)
                {
                    return new ActionResponse
                    {
                        IsSuccess = false,
                        Message = "Announceement not found for this car."
                    };
                }

                // Owner check
                if(announcement.UserDataId  != userId)
                {
                    return new ActionResponse
                    {
                        IsSuccess = false,
                        Message = "Operation failed"
                    };
                }

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

        protected ActionResponse DeleteImageActionExecution(int id, int userId, bool isAdmin)
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

                // Owner check
                if(!isAdmin)
                {
                    var announcement = db.Announcements.FirstOrDefault(a => a.CarId == entity.CarId);
                    if(announcement == null || announcement.UserDataId != userId)
                    {
                        return new ActionResponse
                        {
                            IsSuccess = false,
                            Message = "Operation failed"
                        };
                    }

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
    }
}
