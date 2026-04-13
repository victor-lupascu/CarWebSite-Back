using CarWebSite.BusinessLayer.Interfaces;
using CarWebSite.Domain.Models.Brand;
using Microsoft.AspNetCore.Mvc;

namespace CarWebSite.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandAction _brandAction;

        public BrandController(IBrandAction brandAction)
        {
            _brandAction = brandAction;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var brands = await _brandAction.GetAllBrandsAction();
            return Ok(brands);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var brand = await _brandAction.GetBrandByIdAction(id);
            return brand == null ? NotFound() : Ok(brand);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BrandCreateDto data)
        {
            var response = await _brandAction.CreateBrandAction(data);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _brandAction.DeleteBrandAction(id);
            return Ok(response);
        }
    }
}
