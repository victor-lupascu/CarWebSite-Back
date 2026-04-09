using CarWebSite.Domain.Models.Favorite;
using CarWebSite.Domain.Models.Responses;

namespace CarWebSite.BusinessLayer.Interfaces
{
    public interface IFavoriteAction
    {
        Task<List<FavoriteResponseDto>> GetUserFavoritesAction(int userId);
        Task<ActionResponse> AddFavoriteAction(int carId, int userId);
        Task<ActionResponse> RemoveFavoriteAction(int id);
    }
}

