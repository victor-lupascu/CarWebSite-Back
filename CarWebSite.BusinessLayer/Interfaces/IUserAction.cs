using CarWebSite.Domain.Models.User;

namespace CarWebSite.BusinessLayer.Interfaces
{
    public interface IUserAction
    {
        UserResponseDto? RegisterUserAction(UserRegisterDto data);
        LoginResponseDto? LoginUserAction(UserLoginDto data);
        UserResponseDto? GetUserByIdAction(int id);
    }
}
