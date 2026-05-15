using CarWebSite.BusinessLayer;
using CarWebSite.BusinessLayer.Interfaces;
using CarWebSite.Domain.Models.CarImage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarWebSite.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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
            var (userId, role) = GetCurrentUser();
            var ownerId = _carImageAction.GetCarOwnerAction(data.CarId);
            if (ownerId == null) return NotFound();
            if (ownerId != userId && role != "Admin")
                return Forbid();

            var response = _carImageAction.AddImageAction(data);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var (userId, role) = GetCurrentUser();
            var ownerId = _carImageAction.GetImageOwnerAction(id);
            if (ownerId == null) return NotFound();
            if (ownerId != userId && role != "Admin")
                return Forbid();

            var response = _carImageAction.DeleteImageAction(id);
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
