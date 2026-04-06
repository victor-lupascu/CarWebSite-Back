using CarWebSite.BusinessLayer.Interfaces;
using CarWebSite.Domain.Entities;
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
            var brands = await _brandAction.GetAllAsync();
            return Ok(brands);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var brand = await _brandAction.GetByIdAsync(id);
            return brand == null ? NotFound() : Ok(brand);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Brand brand)
        {
            await _brandAction.AddAsync(brand);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _brandAction.DeleteAsync(id);
            return Ok();
        }
    }
}
