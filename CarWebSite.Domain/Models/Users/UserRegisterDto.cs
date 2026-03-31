using System.ComponentModel.DataAnnotations;

namespace CarWebSite.Domain.Models.User
{
    public class UserRegisterDto
    {
        [Required]
        [StringLength(50)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(200, MinimumLength = 8)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [StringLength(9)]
        public string PhoneNumber { get; set; } = string.Empty;

        [StringLength(50)]
        public string City { get; set; } = string.Empty;
    }
}