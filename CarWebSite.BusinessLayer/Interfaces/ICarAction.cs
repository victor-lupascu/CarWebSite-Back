using CarWebSite.Domain.Models.Car;

namespace CarWebSite.BusinessLayer.Interfaces
{
    public interface ICarAction
    {
        List<CarResponseDto> GetAllCarsAction();
        CarResponseDto? GetCarByIdAction(int id);
    }
}
