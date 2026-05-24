using CarWebSite.Domain.Models.Contact;
using CarWebSite.Domain.Models.Responses;

namespace CarWebSite.BusinessLayer.Interfaces
{
    public interface IContactMessageAction
    {
        ActionResponse SendMessageAction(ContactMessageCreateDto data);
    }
}
