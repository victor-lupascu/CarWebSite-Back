using System.ComponentModel.DataAnnotations;

namespace CarWebSite.Domain.Models.User
{
    public class UserRegisterDto
    {
        [Required]
        [StringLength(70)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(200)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(200, MinimumLength = 8)]
        public string Password { get; set; } = string.Empty;

        [StringLength(20)]
        public string? PhoneNumber { get; set; }

        [StringLength(50)]
        public string? City { get; set; }
    }
}