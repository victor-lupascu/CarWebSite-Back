using System.ComponentModel.DataAnnotations;
using CarWebSite.Domain.Enums;

namespace CarWebSite.Domain.Models.Contact
{
    public class ContactMessageCreateDto
    {
        [Required]
        [StringLength(40)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(80)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public ContactSubject Subject { get; set; }

        [Required]
        [StringLength(1500)]
        public string Message { get; set; } = string.Empty;
    }
}