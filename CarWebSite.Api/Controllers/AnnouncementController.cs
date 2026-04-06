using CarWebSite.BusinessLayer.Interfaces;
using CarWebSite.Domain.Models.Announcement;
using Microsoft.AspNetCore.Mvc;

namespace CarWebSite.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnnouncementController : ControllerBase
    {
        private readonly IAnnouncementAction _announcementAction;

        public AnnouncementController(IAnnouncementAction announcementAction)
        {
            _announcementAction = announcementAction;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var announcements = await _announcementAction.GetAllAnnouncementAction();
            return Ok(announcements);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var announcement = await _announcementAction.GetAnnouncementByIdAction(id);
            return announcement == null ? NotFound() : Ok(announcement);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AnnouncementCreateDto data)
        {
            var response = await _announcementAction.CreateAnnouncementAction(data);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _announcementAction.DeleteAnnouncementAction(id);
            return Ok(response);
        }
    }
}
