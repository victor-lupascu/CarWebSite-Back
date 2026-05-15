using CarWebSite.Domain.Enums;

namespace CarWebSite.Domain.Models.Contact
{
    public class ContactMessageResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public ContactSubject Subject { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
