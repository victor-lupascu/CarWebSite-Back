using CarWebSite.BusinessLayer.Core;
using CarWebSite.BusinessLayer.Interfaces;
using CarWebSite.Domain.Models.Brand;
using CarWebSite.Domain.Models.Responses;

namespace CarWebSite.BusinessLayer.Structure
{
    public class BrandExecution : BrandActions, IBrandAction
    {
        public List<BrandResponseDto> GetAllBrandsAction()
        {
            return GetAllBrandsActionExecution();
        }

        public BrandResponseDto? GetBrandByIdAction(int id)
        {
            return GetBrandByIdActionExecution(id);
        }

        public ActionResponse CreateBrandAction(BrandCreateDto data)
        {
            return CreateBrandActionExecution(data);
        }

        public ActionResponse DeleteBrandAction(int id)
        {
            return DeleteBrandActionExecution(id);
        }
    }
}
