using System.Security.Claims;
using CarWebSite.BusinessLayer;
using CarWebSite.BusinessLayer.Interfaces;
using CarWebSite.Domain.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarWebSite.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserAction _userAction;

        public UserController()
        {
            var bl = new BusinessLogic();
            _userAction = bl.UserAction();
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody] UserRegisterDto data)
        {
            var response = _userAction.RegisterUserAction(data);
            return response == null ? Conflict("Email already registered") : Ok(response);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] UserLoginDto data)
        {
            var response = _userAction.LoginUserAction(data);
            return response == null ? Unauthorized("Invalid email or password") : Ok(response);
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult Me()
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(idClaim, out var userId))
            {
                return Unauthorized();
            }

            var user = _userAction.GetUserByIdAction(userId);
            return user == null ? NotFound() : Ok(user);
        }
    }
}
