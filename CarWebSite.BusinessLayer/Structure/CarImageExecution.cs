using CarWebSite.BusinessLayer.Core;
using CarWebSite.BusinessLayer.Interfaces;
using CarWebSite.Domain.Models.CarImage;
using CarWebSite.Domain.Models.Responses;

namespace CarWebSite.BusinessLayer.Structure
{
    public class CarImageExecution : CarImageActions, ICarImageAction
    {
        public List<CarImageResponseDto> GetImagesByCarAction(int carId)
        {
            return GetImagesByCarActionExecution(carId);
        }

        public ActionResponse AddImageAction(CarImageCreateDto data)
        {
            return AddImageActionExecution(data);
        }

        public ActionResponse DeleteImageAction(int id)
        {
            return DeleteImageActionExecution(id);
        }
    }
}
