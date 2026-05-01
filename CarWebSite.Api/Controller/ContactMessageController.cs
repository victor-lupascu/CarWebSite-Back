using CarWebSite.BusinessLayer;
using CarWebSite.BusinessLayer.Interfaces;
using CarWebSite.Domain.Models.Contact;
using Microsoft.AspNetCore.Mvc;

namespace CarWebSite.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactMessageController : ControllerBase
    {
        private readonly IContactMessageAction _contactAction;

        public ContactMessageController()
        {
            var bl = new BusinessLogic();
            _contactAction = bl.ContactMessageAction();
        }

        [HttpPost]
        public IActionResult Send([FromBody] ContactMessageCreateDto data)
        {
            var response = _contactAction.SendMessageAction(data);
            return Ok(response);
        }
    }
}
