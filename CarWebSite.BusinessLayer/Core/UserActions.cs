using CarWebSite.BusinessLayer.Auth;
using CarWebSite.DataAccess.Context;
using CarWebSite.Domain.Entities;
using CarWebSite.Domain.Enums;
using CarWebSite.Domain.Models.Responses;
using CarWebSite.Domain.Models.User;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace CarWebSite.BusinessLayer.Core
{
    public class UserActions
    {
        protected UserActions() { }

        protected ActionResponse RegisterUserActionExecution(UserRegisterDto dto)
        {
            try
            {
                // Validate password complexity (server-side)
                if (!IsPasswordComplex(dto.Password))
                {
                    return new ActionResponse
                    {
                        IsSuccess = false,
                        Message = "Password must be at least 8 characters" +
                        " and contain uppercase, digit and special character."
                    };
                }

                using (var db = new AppDbContext())
                {
                    // Application level email uniqueness check
                    if (db.Users.Any(u => u.Email == dto.Email))
                    {
                        return new ActionResponse
                        {
                            IsSuccess = false,
                            Message = "An account with this email already exists."
                        };
                    }

                    // Hashing password before storing it
                    var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

                    // Build the new user
                    var newUser = new UserData
                    {
                        FullName = dto.FullName,
                        Email = dto.Email,
                        Password = hashedPassword,
                        PhoneNumber = dto.PhoneNumber,
                        City = dto.City,
                        Role = UserRole.User,
                        RegisteredOn = DateTime.UtcNow,
                        FailedLoginAttempts = 0,
                        LockedUntil = null
                    };

                    // Insert into db
                    db.Users.Add(newUser);
                    db.SaveChanges();

                    return new ActionResponse
                    {
                        IsSuccess = true,
                        Message = "Account created successfully."
                    };
                }
            }
            catch (DbUpdateException ex) when (ex.InnerException?.Message.Contains("IX_Users_Email") == true)
            {
                // Race condition fallback: DB unique index caught a concurrent INSERT
                return new ActionResponse
                {
                    IsSuccess = false,
                    Message = "An account with this email already exists."
                };
            }
        }

        protected AuthResponse LoginUserActionExecution(UserLoginDto dto)
        {
            using (var db = new AppDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Email == dto.Email);
                if (user == null)
                {
                    return new AuthResponse
                    {
                        IsSuccess = false,
                        Message = "Invalid credentials"
                    };
                }

                if (user.LockedUntil.HasValue && user.LockedUntil.Value > DateTime.UtcNow)
                {
                    var minutesLeft = (int)(user.LockedUntil.Value - DateTime.UtcNow).TotalMinutes + 1;
                    return new AuthResponse
                    {
                        IsSuccess = false,
                        Message = $"Account is locked. Try again in {minutesLeft} minute(s)."
                    };
                }

                // Password verification
                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);

                if (!isPasswordValid)
                {
                    user.FailedLoginAttempts++;

                    if(user.FailedLoginAttempts >= 5)
                    {
                        user.LockedUntil = DateTime.UtcNow.AddMinutes(15);
                        user.FailedLoginAttempts = 0;
                    }

                    db.SaveChanges();

                    return new AuthResponse
                    {
                        IsSuccess = false,
                        Message = "Invalid credentials"
                    }; 
                }

                // Reset failed login attempts
                user.FailedLoginAttempts = 0;
                user.LockedUntil = null;

                // Reset token
                var accessToken = TokenService.GenerateAccessToken(user.Id, user.FullName, user.Role.ToString());
                var refreshTokenString = TokenService.GenerateRefreshToken();

                // Save Refresh token in DB
                var refreshToken = new RefreshToken
                {
                    Token = refreshTokenString,
                    UserDataId = user.Id,
                    ExpiresAt = DateTime.UtcNow.AddDays(7),
                    CreatedAt = DateTime.UtcNow
                };
                db.RefreshTokens.Add(refreshToken);
                db.SaveChanges();

                // Return success with token and user info
                return new AuthResponse                   
                {
                    IsSuccess = true,
                    Message = "Login successful.",
                    AccessToken = accessToken,
                    RefreshToken = refreshTokenString,
                    User = new AuthUserDto
                    {
                        Id = user.Id,
                        FullName = user.FullName,
                        Email = user.Email,
                        Role = user.Role.ToString()
                    }
                };

            }
        }

        protected AuthResponse RefreshTokenActionExecution(string refreshTokenString)
        {
            using (var db = new AppDbContext())
            {
                // Find refresh token in DB with user data
                var refreshToken = db.RefreshTokens
                    .Include(rt => rt.UserData)
                    .FirstOrDefault(rt => rt.Token == refreshTokenString);

                // Token doesn't exists in DB
                if (refreshToken == null)
                {
                    return new AuthResponse
                    {
                        IsSuccess = false,
                        Message = "Invalid refresh token."
                    };
                }

                // Tken has been revoked (logout)
                if(refreshToken.RevokedAt != null)
                {
                    return new AuthResponse
                    {
                        IsSuccess = false,
                        Message = "Refresh token has been revoked."
                    };
                }

                //Token has expired 
                if(refreshToken.ExpiresAt <= DateTime.UtcNow)
                {
                    return new AuthResponse
                    {
                        IsSuccess = false,
                        Message = "Refresh Token has expired."
                    };
                }

                // Generate new JWT (refresh token reused)
                var newAccessToken = TokenService.GenerateAccessToken(
                    refreshToken.UserData.Id,
                    refreshToken.UserData.FullName,
                    refreshToken.UserData.Role.ToString());

                return new AuthResponse
                {
                    IsSuccess = true,
                    Message = "Token refreshed.",
                    AccessToken = newAccessToken,
                    RefreshToken = refreshToken.Token,
                    User = new AuthUserDto
                    {
                        Id = refreshToken.UserData.Id,
                        FullName = refreshToken.UserData.FullName,
                        Email = refreshToken.UserData.Email,
                        Role = refreshToken.UserData.Role.ToString()
                    }
                };
            }
        }

        protected ActionResponse LogoutActionExecution(string refreshTokenString)
        {
            using (var db = new AppDbContext())
            {
                // Find the refresh token
                var refreshToken = db.RefreshTokens
                    .FirstOrDefault(rt => rt.Token == refreshTokenString);

                // Revoke it, if exists and not already revoked
                if(refreshToken != null && refreshToken.RevokedAt == null)
                {
                    refreshToken.RevokedAt = DateTime.UtcNow;
                    db.SaveChanges();
                }

                return new ActionResponse
                {
                    IsSuccess = true,
                    Message = "Logged out successfully."
                };
            }
        }

        // Server-side complexity check
        private static bool IsPasswordComplex(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 8)
                return false;

            bool hasUpper = Regex.IsMatch(password, "[A-Z]");
            bool hasDigit = Regex.IsMatch(password, "[0-9]");
            bool hasSpecial = Regex.IsMatch(password, @"[!@#$%^&*(),.?""':{}|<>_\-+=\[\]\\/`~;]");

            return hasUpper && hasDigit && hasSpecial;
        }
    }
}