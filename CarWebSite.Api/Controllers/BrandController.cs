using CarWebSite.BusinessLayer.Structure;
using CarWebSite.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CarWebSite.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly BrandExecution _brandExecution;

        public BrandController(BrandExecution brandExecution)
        {
            _brandExecution = brandExecution;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var brands = await _brandExecution.GetAllBrandsAsync();
            return Ok(brands);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var brand = await _brandExecution.GetBrandByIdAsync(id);
            return brand == null ? NotFound() : Ok(brand);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Brand brand)
        {
            await _brandExecution.CreateBrandAsync(brand);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Brand brand)
        {
            await _brandExecution.UpdateBrandAsync(brand);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _brandExecution.DeleteBrandAsync(id);
            return Ok();
        }
    }
}