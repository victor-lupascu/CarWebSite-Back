using CarWebSite.BusinessLayer.Interfaces;
using CarWebSite.Domain.Models.CarImage;
using Microsoft.AspNetCore.Mvc;

namespace CarWebSite.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarImageController : ControllerBase
    {
        private readonly ICarImageAction _carImageAction;

        public CarImageController(ICarImageAction carImageAction)
        {
            _carImageAction = carImageAction;
        }

        [HttpGet("{carId}")]
        public async Task<IActionResult> GetByCarId(int carId)
        {
            var images = await _carImageAction.GetImagesByCarAction(carId);
            return Ok(images);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CarImageCreateDto data)
        {
            var response = await _carImageAction.AddImageAction(data);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _carImageAction.DeleteImageAction(id);
            return Ok(response);
        }
    }
}
