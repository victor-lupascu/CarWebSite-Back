using CarWebSite.BusinessLayer.Core;
using CarWebSite.BusinessLayer.Interfaces;
using CarWebSite.Domain.Models.User;

namespace CarWebSite.BusinessLayer.Structure
{
    public class UserExecution : UserActions, IUserAction
    {
        public UserResponseDto? RegisterUserAction(UserRegisterDto data)
        {
            return RegisterUserActionExecution(data);
        }

        public LoginResponseDto? LoginUserAction(UserLoginDto data)
        {
            return LoginUserActionExecution(data);
        }

        public UserResponseDto? GetUserByIdAction(int id)
        {
            return GetUserByIdActionExecution(id);
        }
    }
}
