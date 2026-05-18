using CarWebSite.Domain.Models.Responses;
using CarWebSite.Domain.Models.User;

namespace CarWebSite.BusinessLayer.Interfaces
{
    public interface IUserAction
    {
        ActionResponse Register(UserRegisterDto dto);
        AuthResponse Login(UserLoginDto dto);
        AuthResponse Refresh(string refreshToken);
        ActionResponse Logout(string refreshToken);
    }
}