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

        public ActionResponse AddImageAction(CarImageCreateDto data, int userId)
        {
            return AddImageActionExecution(data, userId);
        }

        public ActionResponse DeleteImageAction(int id, int userId, bool isAdmin)
        {
            return DeleteImageActionExecution(id, userId, isAdmin);
        }
    }
}
