using CarWebSite.Domain.Models.CarImage;
using CarWebSite.Domain.Models.Responses;

namespace CarWebSite.BusinessLayer.Interfaces
{
    public interface ICarImageAction
    {
        Task<List<CarImageResponseDto>> GetImagesByCarAction(int carId);
        Task<ActionResponse> AddImageAction(CarImageCreateDto data);
        Task<ActionResponse> DeleteImageAction(int id);
    }
}
