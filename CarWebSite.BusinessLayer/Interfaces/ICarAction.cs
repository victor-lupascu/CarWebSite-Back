using CarWebSite.Domain.Models.Car;

namespace CarWebSite.BusinessLayer.Interfaces
{
    public interface ICarAction
    {
        Task<List<CarResponseDto>> GetAllCarsAction();
        Task<CarResponseDto?> GetCarByIdAction(int id);
    }
}
