using CarWebSite.BusinessLayer.Core;
using CarWebSite.BusinessLayer.Interfaces;
using CarWebSite.Domain.Models.Contact;
using CarWebSite.Domain.Models.Responses;

namespace CarWebSite.BusinessLayer.Structure
{
    public class ContactMessageExecution : ContactMessageActions, IContactMessageAction
    {
        public ActionResponse SendMessageAction(ContactMessageCreateDto data)
        {
            return SendMessageActionExecution(data);
        }

        public List<ContactMessageResponseDto> GetAllMessagesAction()
        {
            return GetAllMessagesActionExecution();
        }
    }
}
