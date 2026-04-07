using CarWebSite.DataAccess.Repositories.Interfaces;
using CarWebSite.BusinessLayer.Interfaces;
using CarWebSite.Domain.Entities;
using CarWebSite.Domain.Models.Contact;
using CarWebSite.Domain.Models.Responses;

namespace CarWebSite.BusinessLayer.Core
{
    public class ContactMessageActions : IContactMessageAction
    {
        private readonly IContactMessageRepository _repository;

        public ContactMessageActions(IContactMessageRepository repository)
        {
            _repository = repository;
        }

        public async Task<ActionResponse> SendMessageAction(ContactMessageCreateDto data)
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

            var entity = new ContactMessageData
            {
                Name = data.Name,
                Email = data.Email,
                Subject = data.Subject,
                Message = data.Message,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(entity);

            return new ActionResponse
            {
                IsSuccess = true,
                Message = "Message sent successfully."
            };
        }
    }
}
