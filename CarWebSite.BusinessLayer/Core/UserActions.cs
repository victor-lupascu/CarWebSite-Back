using CarWebSite.DataAccess.Context;
using CarWebSite.Domain.Entities;
using CarWebSite.Domain.Enums;
using CarWebSite.Domain.Models.User;

namespace CarWebSite.BusinessLayer.Core
{
    public class UserActions
    {
        protected UserActions() { }

        protected UserResponseDto? RegisterUserActionExecution(UserRegisterDto data)
        {
            using (var db = new AppDbContext())
            {
                var existing = db.Users.FirstOrDefault(u => u.Email == data.Email);
                if (existing != null) return null;

                var user = new UserData
                {
                    FullName = data.FullName,
                    Email = data.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(data.Password),
                    PhoneNumber = data.PhoneNumber,
                    City = data.City,
                    Role = UserRole.User,
                    RegisteredOn = DateTime.UtcNow
                };

                db.Users.Add(user);
                db.SaveChanges();

                return MapToDto(user);
            }
        }

        protected LoginResponseDto? LoginUserActionExecution(UserLoginDto data)
        {
            using (var db = new AppDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Email == data.Email);
                if (user == null) return null;

                if (!BCrypt.Net.BCrypt.Verify(data.Password, user.Password)) return null;

                var token = new TokenService().GenerateToken(user.Id, user.Email, user.Role);

                return new LoginResponseDto
                {
                    Token = token,
                    User = MapToDto(user)
                };
            }
        }

        protected UserResponseDto? GetUserByIdActionExecution(int id)
        {
            using (var db = new AppDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Id == id);
                if (user == null) return null;
                return MapToDto(user);
            }
        }

        private UserResponseDto MapToDto(UserData user)
        {
            return new UserResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber ?? string.Empty,
                City = user.City ?? string.Empty,
                Role = user.Role.ToString(),
                RegisteredOn = user.RegisteredOn
            };
        }
    }
}
