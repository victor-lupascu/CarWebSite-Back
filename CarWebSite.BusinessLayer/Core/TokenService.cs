using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CarWebSite.Domain.Enums;
using Microsoft.IdentityModel.Tokens;

namespace CarWebSite.BusinessLayer.Core
{
    public class TokenService
    {
        private const string Issuer = "CarWebSiteApi";
        private const string Audience = "CarWebSiteClients";
        // BCrypt is used for password hashing (not here) because it has automatic salt and
        // is intentionally slow — resistant to rainbow tables and brute-force attacks,
        // unlike SHA256 which is fast and saltless by default.
        private const string SecretKey = "carwebsite_tweb_2026_super_secret_key_min_32_chars!!";
        private const int ExpireMinutes = 60;

        public string GenerateToken(int userId, string email, UserRole role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(ExpireMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
