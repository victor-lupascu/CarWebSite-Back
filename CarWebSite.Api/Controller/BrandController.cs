using CarWebSite.BusinessLayer;
using CarWebSite.BusinessLayer.Interfaces;
using CarWebSite.Domain.Models.Brand;
using Microsoft.AspNetCore.Mvc;

namespace CarWebSite.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : ControllerBase
    {
        private IBrandAction _brandAction;

        public BrandController()
        {
            var bl = new BusinessLogic();
            _brandAction = bl.BrandAction();
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var brands = _brandAction.GetAllBrandsAction();
            return Ok(brands);
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var brand = _brandAction.GetBrandByIdAction(id);
            return brand == null ? NotFound() : Ok(brand);
        }

        [HttpPost]
        public IActionResult Create([FromBody] BrandCreateDto data)
        {
            var response = _brandAction.CreateBrandAction(data);
            return Ok(response);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var response = _brandAction.DeleteBrandAction(id);
            return Ok(response);
        }

    }
}