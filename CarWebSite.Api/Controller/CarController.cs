using CarWebSite.BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarWebSite.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICarAction _carAction;

        public CarController(ICarAction carAction)
        {
            _carAction = carAction;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cars = await _carAction.GetAllCarsAction();
            return Ok(cars);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var car = await _carAction.GetCarByIdAction(id);
            return car == null ? NotFound() : Ok(car);
        }
    }
}
