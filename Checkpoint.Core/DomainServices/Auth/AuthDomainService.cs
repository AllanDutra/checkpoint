using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Checkpoint.Core.Models.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Checkpoint.Core.DomainServices.Auth
{
    public class AuthDomainService : IAuthDomainService
    {
        private readonly IConfiguration _configuration;

        public AuthDomainService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(EmployeeClaimsViewModel employeeClaims)
        {
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = _configuration["Jwt:Key"];

            var expiryTime = int.Parse(_configuration["Jwt:ExpiryTimeInHours"]);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("Id", employeeClaims.Id.ToString()),
                new Claim("Email", employeeClaims.Email),
                new Claim("Name", employeeClaims.Name),
                new Claim("User", employeeClaims.User),
                new Claim("VerifiedEmail", employeeClaims.VerifiedEmail.ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                expires: DateTime.Now.AddHours(expiryTime),
                signingCredentials: credentials,
                claims: claims
            );

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }

        public EmployeeClaimsViewModel ReadUserClaims(IEnumerable<Claim> userClaims)
        {
            return new EmployeeClaimsViewModel(
                int.Parse(userClaims.First(c => c.Type == "Id").Value),
                userClaims.First(c => c.Type == "Email").Value,
                userClaims.First(c => c.Type == "Name").Value,
                userClaims.First(c => c.Type == "User").Value,
                bool.Parse(userClaims.First(c => c.Type == "VerifiedEmail").Value)
            );
        }

        public string GenerateEmailConfirmationCode()
        {
            const int LENGTH = 6; // ? NUMBER OF DIGITS

            var random = new Random();

            var confirmationCode = new StringBuilder();

            for (int i = 0; i < LENGTH; i++)
            {
                var randomValue = random.Next(36); // ? Generate a random value between 0 and 35 (26 letters + 10 numbers)

                var character = (char)(
                    randomValue < 10 ? '0' + randomValue : 'A' + randomValue - 10
                );

                confirmationCode.Append(character);
            }

            return confirmationCode.ToString();
        }
    }
}
