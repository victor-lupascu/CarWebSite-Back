using CarWebSite.BusinessLayer.Interfaces;
using CarWebSite.Domain.Models.Contact;
using Microsoft.AspNetCore.Mvc;

namespace CarWebSite.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactMessageController : ControllerBase
    {
        private readonly IContactMessageAction _contactAction;

        public ContactMessageController(IContactMessageAction contactAction)
        {
            _contactAction = contactAction;
        }

        [HttpPost]
        public async Task<IActionResult> Send([FromBody] ContactMessageCreateDto data)
        {
            var response = await _contactAction.SendMessageAction(data);
            return Ok(response);
        }
    }
}
