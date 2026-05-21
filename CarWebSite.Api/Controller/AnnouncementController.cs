using CarWebSite.BusinessLayer;
using CarWebSite.BusinessLayer.Interfaces;
using CarWebSite.Domain.Models.Announcement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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
            var response = _announcementAction.CreateAnnouncementAction(data);
            return Ok(response);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var response = _announcementAction.DeleteAnnouncementAction(id);
            return Ok(response);
        }
    }
}
