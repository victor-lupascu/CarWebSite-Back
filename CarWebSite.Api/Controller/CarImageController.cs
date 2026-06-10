using CarWebSite.BusinessLayer;
using CarWebSite.BusinessLayer.Interfaces;
using CarWebSite.Domain.Models.CarImage;
using CarWebSite.Domain.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarWebSite.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarImageController : ControllerBase
    {
        private readonly ICarImageAction _carImageAction;

        public CarImageController()
        {
            var bl = new BusinessLogic();
            _carImageAction = bl.CarImageAction();
        }

        [HttpGet("{carId}")]
        [AllowAnonymous]
        public IActionResult GetByCarId(int carId)
        {
            var images = _carImageAction.GetImagesByCarAction(carId);
            return Ok(images);
        }

        [HttpPost]
        public IActionResult Add([FromBody] CarImageCreateDto data)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var response = _carImageAction.AddImageAction(data, userId);
            return ToHttpResponse(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var isAdmin = User.IsInRole("Admin");
            var response = _carImageAction.DeleteImageAction(id, userId, isAdmin);
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

            if (message.Contains("operation failed", StringComparison.OrdinalIgnoreCase))
            {
                return StatusCode(StatusCodes.Status403Forbidden, response);
            }

            return BadRequest(response);
        }
    }
}
