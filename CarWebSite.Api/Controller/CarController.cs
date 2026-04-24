using CarWebSite.BusinessLayer;
using CarWebSite.BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarWebSite.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private ICarAction _carAction;

        public CarController()
        {
            var bl = new BusinessLogic();
            _carAction = bl.CarAction();
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var cars = _carAction.GetAllCarsAction();
            return Ok(cars);
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var car = _carAction.GetCarByIdAction(id);
            return car == null ? NotFound() : Ok(car);
        }
    }
}
