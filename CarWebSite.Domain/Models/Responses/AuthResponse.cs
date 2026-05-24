using CarWebSite.Domain.Models.User;

namespace CarWebSite.Domain.Models.Responses
{
    public class AuthResponse : ActionResponse
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public AuthUserDto? User { get; set; }

    }
}
