using CarWebSite.BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarWebSite.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class  FavoriteController : ControllerBase
    {
        private readonly IFavoriteAction _favoriteAction;

        public FavoriteController(IFavoriteAction favoriteAction)
        {
            _favoriteAction = favoriteAction;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserFavorites(int userId)
        {
            var result = await _favoriteAction.GetUserFavoritesAction(userId);
            return Ok(result);
        }

        [HttpPost("{carId}/{userId}")]
        public async Task<IActionResult> AddFavorite(int carId, int userId)
        { var response = await _favoriteAction.AddFavoriteAction(carId, userId);
        return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFavorite(int id)
        { var response = await _favoriteAction.RemoveFavoriteAction(id);
            return Ok(response);
        }
    }
}
