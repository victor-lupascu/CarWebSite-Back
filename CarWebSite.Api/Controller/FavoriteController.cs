using CarWebSite.BusinessLayer;
using CarWebSite.BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarWebSite.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoriteController : ControllerBase
    {
        private IFavoriteAction _favoriteAction;

        public FavoriteController()
        {
            var bl = new BusinessLogic();
            _favoriteAction = bl.FavoriteAction();
        }

        [HttpGet("{userId}")]
        public IActionResult GetByUser(int userId)
        {
            var favorites = _favoriteAction.GetUserFavoritesAction(userId);
            return Ok(favorites);
        }

        [HttpPost]
        public IActionResult Add([FromQuery] int carId, [FromQuery] int userId)
        {
            var response = _favoriteAction.AddFavoriteAction(carId, userId);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            var response = _favoriteAction.RemoveFavoriteAction(id);
            return Ok(response);
        }
    }
}
