using CarWebSite.BusinessLayer;
using CarWebSite.BusinessLayer.Interfaces;
using CarWebSite.Domain.Models.Brand;
using CarWebSite.Domain.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarWebSite.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandAction _brandAction;

        public BrandController()
        {
            var bl = new BusinessLogic();
            _brandAction = bl.BrandAction();
        }

        [HttpGet("getAll")]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            var brands = _brandAction.GetAllBrandsAction();
            return Ok(brands);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetById(int id)
        {
            var brand = _brandAction.GetBrandByIdAction(id);
            return brand == null ? NotFound(new { message = "Brand not found." }) : Ok(brand);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody] BrandCreateDto data)
        {
            var response = _brandAction.CreateBrandAction(data);
            return ToHttpResponse(response);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var response = _brandAction.DeleteBrandAction(id);
            return ToHttpResponse(response);
        }

        private IActionResult ToHttpResponse(ActionResponse response)
        {
            if (response.IsSuccess) return Ok(response);
            var message = response.Message ?? "Operation failed.";
            return message.Contains("not found", StringComparison.OrdinalIgnoreCase)
                ? NotFound(response)
                : BadRequest(response);
        }
    }
}
