using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DataLayer.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Core.Services
{
    public class AuthorizationService
    {
        #region Fields
        private readonly string _securityKey;
        #endregion

        #region Constructors
        public AuthorizationService(IConfiguration config)
        {
            _securityKey = config["JWT:SecurityKey"];
        }
        #endregion

        #region Public members
        public string GetToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            /* Generate keys online
             * 128-bit  
             * https://www.allkeysgenerator.com/Random/Security-Encryption-Key-Generator.aspx
            */

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_securityKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var roleClaim = new Claim("role", user.Role);
            var idClaim = new Claim("userId", user.Id.ToString());
            var infoClaim = new Claim("username", user.Email);

            var tokenDescriptior = new SecurityTokenDescriptor
            {
                Issuer = "Backend",
                Audience = "Frontend",
                Subject = new ClaimsIdentity(new[] { roleClaim, idClaim, infoClaim }),
                Expires = DateTime.Now.AddYears(1),
                SigningCredentials = credentials
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptior);
            var tokenString = jwtTokenHandler.WriteToken(token);

            return tokenString;
        }

        public bool ValidateToken(string tokenString)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_securityKey));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                IssuerSigningKey = key,
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
            };

            if (!jwtTokenHandler.CanReadToken(tokenString.Replace("Bearer ", string.Empty)))
            {
                Console.WriteLine("Invalid Token");
                return false;
            }

            jwtTokenHandler.ValidateToken(tokenString, tokenValidationParameters, out var validatedToken);
            return validatedToken != null;
        }
        #endregion
    }
}
