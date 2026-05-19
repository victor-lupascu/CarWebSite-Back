using System.ComponentModel.DataAnnotations;

namespace CarWebSite.Domain.Models.User
{
    public class RefreshTokenRequestDto
    {
        [Required]
        public string RefreshToken { get; set; } = string.Empty;
    }
}
