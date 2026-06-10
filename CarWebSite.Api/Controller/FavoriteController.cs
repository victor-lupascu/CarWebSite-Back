using CarWebSite.BusinessLayer;
using CarWebSite.BusinessLayer.Interfaces;
using CarWebSite.Domain.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarWebSite.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteAction _favoriteAction;

        public FavoriteController()
        {
            var bl = new BusinessLogic();
            _favoriteAction = bl.FavoriteAction();
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetMyFavorites()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var favorites = _favoriteAction.GetUserFavoritesAction(userId);
            return Ok(favorites);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add([FromQuery] int carId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var response = _favoriteAction.AddFavoriteAction(carId, userId);
            return ToHttpResponse(response);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var response = _favoriteAction.RemoveFavoriteAction(id, userId);
            return ToHttpResponse(response);
        }

        private IActionResult ToHttpResponse(ActionResponse response)
        {
            if (response.IsSuccess) return Ok(response);

            var message = response.Message ?? "Operation failed.";
            if (message.Contains("not found", StringComparison.OrdinalIgnoreCase))
            {
                return NotFound(response);
            }

            if (message.Contains("invalid operation", StringComparison.OrdinalIgnoreCase))
            {
                return StatusCode(StatusCodes.Status403Forbidden, response);
            }

            return BadRequest(response);
        }
    }
}
