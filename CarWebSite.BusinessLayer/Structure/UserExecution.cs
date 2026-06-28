using CarWebSite.BusinessLayer.Core;
using CarWebSite.BusinessLayer.Interfaces;
using CarWebSite.Domain.Models.Responses;
using CarWebSite.Domain.Models.User;

namespace CarWebSite.BusinessLayer.Structure
{
    public class UserExecution : UserActions, IUserAction
    {
        public ActionResponse Register(UserRegisterDto dto)
        {
            return RegisterUserActionExecution(dto);
        }

        public AuthResponse Login(UserLoginDto dto)
        {
            return LoginUserActionExecution(dto);
        }

        public AuthResponse Refresh(string refreshToken)
        {
            return RefreshTokenActionExecution(refreshToken);
        }

        public ActionResponse Logout(string refreshToken)
        {
            return LogoutActionExecution(refreshToken);
        }

        public ProfileResponse UpdateProfile(UserProfileUpdateDto dto, int userId)
        {
            return UpdateProfileActionExecution(dto, userId);
        }

        public ProfileResponse GetProfile(int userId)
        {
            return GetProfileActionExecution(userId);
        }
    }
}