using CarWebSite.Domain.Models.Brand;
using CarWebSite.Domain.Models.Responses;

namespace CarWebSite.BusinessLayer.Interfaces
{
    public interface IBrandAction
    {
        Task<List<BrandResponseDto>> GetAllBrandsAction();
        Task<BrandResponseDto?> GetBrandByIdAction(int id);
        Task<ActionResponse> CreateBrandAction(BrandCreateDto data);
        Task<ActionResponse> DeleteBrandAction(int id);
    }
}
