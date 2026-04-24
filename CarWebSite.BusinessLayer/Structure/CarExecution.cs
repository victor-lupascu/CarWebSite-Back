using CarWebSite.BusinessLayer.Core;
using CarWebSite.BusinessLayer.Interfaces;
using CarWebSite.Domain.Models.Car;

namespace CarWebSite.BusinessLayer.Structure
{
    public class CarExecution : CarActions, ICarAction
    {
        public List<CarResponseDto> GetAllCarsAction()
        {
            return GetAllCarsActionExecution();
        }

        public CarResponseDto? GetCarByIdAction(int id)
        {
            return GetCarByIdActionExecution(id);
        }
    }
}
