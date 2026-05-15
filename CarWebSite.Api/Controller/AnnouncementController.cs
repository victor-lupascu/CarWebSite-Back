using CarWebSite.BusinessLayer;
using CarWebSite.BusinessLayer.Interfaces;
using CarWebSite.Domain.Models.Announcement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarWebSite.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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
            return announcement == null ? NotFound() : Ok(announcement);
        }

        [HttpPost]
        public IActionResult Create([FromBody] AnnouncementCreateDto data)
        {
            var (userId, _) = GetCurrentUser();
            data.UserId = userId;
            var response = _announcementAction.CreateAnnouncementAction(data);
            return Ok(response);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var announcement = _announcementAction.GetAnnouncementByIdAction(id);
            if (announcement == null) return NotFound();

            var (userId, role) = GetCurrentUser();
            if (announcement.UserId != userId && role != "Admin")
                return Forbid();

            var response = _announcementAction.DeleteAnnouncementAction(id);
            return Ok(response);
        }

        private (int userId, string role) GetCurrentUser()
        {
            var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var role = User.FindFirst(ClaimTypes.Role)!.Value;
            return (id, role);
        }
    }
}
