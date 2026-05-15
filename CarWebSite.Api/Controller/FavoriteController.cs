using CarWebSite.BusinessLayer;
using CarWebSite.BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarWebSite.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteAction _favoriteAction;

        public FavoriteController()
        {
            var bl = new BusinessLogic();
            _favoriteAction = bl.FavoriteAction();
        }

        [HttpGet]
        public IActionResult GetByUser()
        {
            var (userId, _) = GetCurrentUser();
            var favorites = _favoriteAction.GetUserFavoritesAction(userId);
            return Ok(favorites);
        }

        [HttpPost]
        public IActionResult Add([FromQuery] int carId)
        {
            var (userId, _) = GetCurrentUser();
            var response = _favoriteAction.AddFavoriteAction(carId, userId);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            var (userId, _) = GetCurrentUser();
            var ownerId = _favoriteAction.GetFavoriteOwnerAction(id);
            if (ownerId == null) return NotFound();
            if (ownerId != userId)
                return Forbid();

            var response = _favoriteAction.RemoveFavoriteAction(id);
            return Ok(response);
        }

        private (int userId, string role) GetCurrentUser()
        {
            var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var role = User.FindFirst(ClaimTypes.Role)!.Value;
            return (id, role);
        }
    }
}
