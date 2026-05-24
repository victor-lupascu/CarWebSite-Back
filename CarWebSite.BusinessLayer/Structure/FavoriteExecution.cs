using CarWebSite.BusinessLayer.Core;
using CarWebSite.BusinessLayer.Interfaces;
using CarWebSite.Domain.Models.Favorite;
using CarWebSite.Domain.Models.Responses;

namespace CarWebSite.BusinessLayer.Structure
{
    public class FavoriteExecution : FavoriteActions, IFavoriteAction
    {
        public List<FavoriteResponseDto> GetUserFavoritesAction(int userId)
        {
            return GetUserFavoritesActionExecution(userId);
        }

        public ActionResponse AddFavoriteAction(int carId, int userId)
        {
            return AddFavoriteActionExecution(carId, userId);
        }

        public ActionResponse RemoveFavoriteAction(int id, int userId)
        {
            return RemoveFavoriteActionExecution(id, userId);
        }
    }
}
