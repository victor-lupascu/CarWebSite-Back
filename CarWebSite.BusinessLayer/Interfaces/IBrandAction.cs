using CarWebSite.Domain.Models.Brand;
using CarWebSite.Domain.Models.Responses;

namespace CarWebSite.BusinessLayer.Interfaces
{ 
    public interface IBrandAction
    {
        List<BrandResponseDto> GetAllBrandsAction();
        BrandResponseDto? GetBrandByIdAction(int id);
        ActionResponse CreateBrandAction(BrandCreateDto data);
        ActionResponse DeleteBrandAction(int id);
    }
}
