using System.ComponentModel.DataAnnotations;

namespace CarWebSite.Domain.Models.User
{
    public class UserProfileUpdateDto
    {
        [StringLength(70)]
        public string? FullName { get; set; }

        [StringLength(20)]
        public string? PhoneNumber { get; set; }

        [StringLength (50)]
        public string? City { get; set; }
    }
}
