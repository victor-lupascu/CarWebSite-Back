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
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var isAdmin = User.IsInRole("Admin");
            var response = _carImageAction.DeleteImageAction(id, userId, isAdmin);
            return Ok(response);
        }
    }
}
