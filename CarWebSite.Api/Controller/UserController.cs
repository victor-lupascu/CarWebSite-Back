using CarWebSite.BusinessLayer;
using CarWebSite.BusinessLayer.Interfaces;
using CarWebSite.Domain.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace CarWebSite.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserAction _userAction;

        public UserController()
        {
            var bl = new BusinessLogic();
            _userAction = bl.UserAction();
        }

        [HttpGet("me")]
        public IActionResult GetProfile()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var response = _userAction.GetProfile(userId);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }


        [HttpPatch("me")]
        public IActionResult UpdateProfile([FromBody] UserProfileUpdateDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var response = _userAction.UpdateProfile(dto, userId);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
