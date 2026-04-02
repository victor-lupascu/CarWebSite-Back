using CarWebSite.BusinessLayer.Structure;
using CarWebSite.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CarWebSite.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnnouncementController : ControllerBase
    {
        private readonly AnnouncementExecution _announcementExecution;

        public AnnouncementController(AnnouncementExecution announcementExecution)
        {
            _announcementExecution = announcementExecution;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var announcements = await _announcementExecution.GetAllAnnouncementsAsync();
            return Ok(announcements);
        }

        [HttpGet("details")]
        public async Task<IActionResult> GetAllWithDetails()
        {
            var announcements = await _announcementExecution.GetAllWithDetailsAsync();
            return Ok(announcements);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var announcement = await _announcementExecution.GetAnnouncementByIdAsync(id);
            return announcement == null ? NotFound() : Ok(announcement);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Announcement announcement)
        {
            await _announcementExecution.CreateAnnouncementAsync(announcement);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Announcement announcement)
        {
            await _announcementExecution.UpdateAnnouncementAsync(announcement);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _announcementExecution.DeleteAnnouncementAsync(id);
            return Ok();
        }
    }
}