using CarWebSite.Domain.Models.User;

namespace CarWebSite.Domain.Models.Responses
{
    public class ProfileResponse : ActionResponse
    {
        public UserResponseDto? User { get; set; }
    }
}
