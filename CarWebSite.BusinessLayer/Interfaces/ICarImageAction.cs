using CarWebSite.Domain.Models.CarImage;
using CarWebSite.Domain.Models.Responses;

namespace CarWebSite.BusinessLayer.Interfaces
{
    public interface ICarImageAction
    {
        List<CarImageResponseDto> GetImagesByCarAction(int carId);
        ActionResponse AddImageAction(CarImageCreateDto data);
        ActionResponse DeleteImageAction(int id);
        int? GetCarOwnerAction(int carId);
        int? GetImageOwnerAction(int imageId);
    }
}
