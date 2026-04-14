using CarWebSite.DataAccess.Context;
using CarWebSite.Domain.Entities;
using CarWebSite.Domain.Models.Brand;
using CarWebSite.Domain.Models.Responses;

namespace CarWebSite.BusinessLayer.Core
{
    public class BrandActions
    {
        protected BrandActions() { }

        protected List<BrandResponseDto> GetAllBrandsActionExecution()
        {
            var data = new List<BrandResponseDto>();
            List<Brand> brands;

            using (var db = new AppDbContext())
            {
                brands = db.Brands.ToList();
            }

            if (brands.Count <= 0) return data;

            foreach (var item in brands)
            {
                data.Add(new BrandResponseDto
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            return data;
        }

        protected BrandResponseDto? GetBrandByIdActionExecution(int id)
        {
            Brand? entity;

            using (var db = new AppDbContext())
            {
                entity = db.Brands.FirstOrDefault(b => b.Id == id);
            }

            if (entity == null) return null;

            return new BrandResponseDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        protected ActionResponse CreateBrandActionExecution(BrandCreateDto data)
        {
            if (string.IsNullOrWhiteSpace(data.Name))
            {
                return new ActionResponse
                {
                    IsSuccess = false,
                    Message = "Brand name is required."
                };
            }

            using (var db = new AppDbContext())
            {
                var existing = db.Brands
                    .FirstOrDefault(b => b.Name.ToLower() == data.Name.ToLower());

                if (existing != null)
                {
                    return new ActionResponse
                    {
                        IsSuccess = false,
                        Message = "Brand already exists."
                    };
                }

                var entity = new Brand
                {
                    Name = data.Name
                };

                db.Brands.Add(entity);
                db.SaveChanges();
            }
            return new ActionResponse
            {
                IsSuccess = true,
                Message = "Brand created successfully."
            };
        }
        
        protected ActionResponse DeleteBrandActionExecution(int id)
        {
            using (var db = new AppDbContext())
            {
                var entity = db.Brands.FirstOrDefault(b => b.Id == id);
                if(entity == null)
                {
                    return new ActionResponse
                    {
                        IsSuccess = false,
                        Message = "Brand not found"
                    };
                }

                db.Brands.Remove(entity);
                db.SaveChanges();
            }

            return new ActionResponse
            {
                IsSuccess = true,
                Message = "Brand deleted successfully."
            };
        }
    }
}