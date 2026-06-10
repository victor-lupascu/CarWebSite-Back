using CarWebSite.BusinessLayer;
using CarWebSite.BusinessLayer.Interfaces;
using CarWebSite.Domain.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace CarWebSite.Api.Controller
{

    [ApiController]
    [Route("api/[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly IUserAction _userAction;

        public SessionController()
        {
            var bl = new BusinessLogic();
            _userAction = bl.UserAction();
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody] UserRegisterDto dto)
        {
            var response = _userAction.Register(dto);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPost("auth")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] UserLoginDto dto)
        {
            var response = _userAction.Login(dto);
            return response.IsSuccess ? Ok(response) : Unauthorized(response);
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        public IActionResult Refresh([FromBody] RefreshTokenRequestDto dto)
        {
            var response = _userAction.Refresh(dto.RefreshToken);
            return response.IsSuccess ? Ok(response) : Unauthorized(response);
        }

        [HttpPost("logout")]
        public IActionResult Logout([FromBody] RefreshTokenRequestDto dto)
        {
            var response = _userAction.Logout(dto.RefreshToken);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
