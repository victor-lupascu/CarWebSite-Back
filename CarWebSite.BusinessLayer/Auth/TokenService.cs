using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CarWebSite.BusinessLayer.Auth
{
    public class TokenService
    {
        public TokenService() { }

        public string GenerateAccessToken(int userId, string fullName, string role)
        {
            // Build Symetric signing key from secret
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSession.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);



            // Claims - informations for payload
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Name, fullName),
                new Claim(ClaimTypes.Role, role)
            };


            // Assemble JWT construction
            var token = new JwtSecurityToken(
                issuer: JwtSession.Issuer,
                audience: JwtSession.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(JwtSession.AccessTokenMinutes),
                signingCredentials: creds);


            // Serialize the token to its Header.Payload.Signature string form
            return new JwtSecurityTokenHandler().WriteToken(token);
        

        }

        public string GenerateRefreshToken()
        {
            // Random bytes ensure refresh tokens
            var bytes = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
            }
            return Convert.ToBase64String(bytes);

        }
    }
}
