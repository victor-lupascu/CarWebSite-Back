using CarWebSite.Domain.Models.CarImage;
using CarWebSite.Domain.Models.Responses;

namespace CarWebSite.BusinessLayer.Interfaces
{
    public interface ICarImageAction
    {
        List<CarImageResponseDto> GetImagesByCarAction(int carId);
        ActionResponse AddImageAction(CarImageCreateDto data, int userId);
        ActionResponse DeleteImageAction(int id, int userId, bool isAdmin);
    }
}
