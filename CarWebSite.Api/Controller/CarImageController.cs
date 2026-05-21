using CarWebSite.BusinessLayer;
using CarWebSite.BusinessLayer.Interfaces;
using CarWebSite.Domain.Models.CarImage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            var response = _carImageAction.AddImageAction(data);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var response = _carImageAction.DeleteImageAction(id);
            return Ok(response);
        }
    }
}
