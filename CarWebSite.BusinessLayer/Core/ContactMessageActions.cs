using CarWebSite.DataAccess.Context;
using CarWebSite.Domain.Entities;
using CarWebSite.Domain.Models.Contact;
using CarWebSite.Domain.Models.Responses;

namespace CarWebSite.BusinessLayer.Core
{
    public class ContactMessageActions
    {
        protected ContactMessageActions() { }

        protected ActionResponse SendMessageActionExecution(ContactMessageCreateDto data)
        {
            if (string.IsNullOrWhiteSpace(data.Name))
            {
                return new ActionResponse
                {
                    IsSuccess = false,
                    Message = "Name is required."
                };
            }

            if (string.IsNullOrWhiteSpace(data.Email))
            {
                return new ActionResponse
                {
                    IsSuccess = false,
                    Message = "Email is required."
                };
            }

            if (string.IsNullOrWhiteSpace(data.Message))
            {
                return new ActionResponse
                {
                    IsSuccess = false,
                    Message = "Message text is required."
                };
            }

            using (var db = new AppDbContext())
            {
                var entity = new ContactMessageData
                {
                    Name = data.Name,
                    Email = data.Email,
                    Subject = data.Subject,
                    Message = data.Message,
                    CreatedAt = DateTime.UtcNow
                };

                db.ContactMessages.Add(entity);
                db.SaveChanges();
            }

            return new ActionResponse
            {
                IsSuccess = true,
                Message = "Message sent successfully."
            };
        }
    }
}
