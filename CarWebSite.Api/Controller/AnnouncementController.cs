using CarWebSite.BusinessLayer;
using CarWebSite.BusinessLayer.Interfaces;
using CarWebSite.Domain.Models.Announcement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using CarWebSite.Domain.Models.Responses;

namespace CarWebSite.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnnouncementController : ControllerBase
    {
        private readonly IAnnouncementAction _announcementAction;

        public AnnouncementController()
        {
            var bl = new BusinessLogic();
            _announcementAction = bl.AnnouncementAction();
        }

        [HttpGet("getAll")]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            var announcements = _announcementAction.GetAllAnnouncementAction();
            return Ok(announcements);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetById(int id)
        {
            var announcement = _announcementAction.GetAnnouncementByIdAction(id);
            return announcement == null
                ? NotFound(new { message = "Announcement not found." })
                : Ok(announcement);
        }

        [HttpPost]
        public IActionResult Create([FromBody] AnnouncementCreateDto data)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var response = _announcementAction.CreateAnnouncementAction(data, userId);

            if(response == null)
            {
                return BadRequest(new { message = "Invalid announcement data." });
            }

            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] AnnouncementUpdateDto data)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var isAdmin = User.IsInRole("Admin");
            var response = _announcementAction.UpdateAnnouncementAction(id, data, userId, isAdmin);
            return ToHttpResponse(response);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var isAdmin = User.IsInRole("Admin");
            var response = _announcementAction.DeleteAnnouncementAction(id, userId, isAdmin);
            return ToHttpResponse(response);
        }

        private IActionResult ToHttpResponse(ActionResponse response)
        {
            if (response.IsSuccess) return Ok(response);

            var message = response.Message ?? "Operation failed.";
            if (message.Contains("not found", StringComparison.OrdinalIgnoreCase))
            {
                return NotFound(response);
            }

            if (message.Contains("operation failed", StringComparison.OrdinalIgnoreCase))
            {
                return StatusCode(StatusCodes.Status403Forbidden, response);
            }

            return BadRequest(response);
        }
    }
}
