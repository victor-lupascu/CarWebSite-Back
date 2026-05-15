using CarWebSite.Domain.Models.Favorite;
using CarWebSite.Domain.Models.Responses;

namespace CarWebSite.BusinessLayer.Interfaces
{
    public interface IFavoriteAction
    {
        List<FavoriteResponseDto> GetUserFavoritesAction(int userId);
        ActionResponse AddFavoriteAction(int carId, int userId);
        ActionResponse RemoveFavoriteAction(int id);
        int? GetFavoriteOwnerAction(int id);
    }
}

