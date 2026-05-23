using CarWebSite.BusinessLayer;
using CarWebSite.BusinessLayer.Interfaces;
using CarWebSite.Domain.Models.Announcement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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
            return announcement == null ? NotFound() : Ok(announcement);
        }

        [HttpPost]
        public IActionResult Create([FromBody] AnnouncementCreateDto data)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var response = _announcementAction.CreateAnnouncementAction(data, userId);
            return Ok(response);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var isAdmin = User.IsInRole("Admin");
            var response = _announcementAction.DeleteAnnouncementAction(id, userId, isAdmin);
            return Ok(response);
        }
    }
}
