using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CutInLine.Models.Class;
using Microsoft.IdentityModel.Tokens;


namespace CutInLine.Services
{
    public static class TokenService
    {
        public static string GerarToken(Users user)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var key = Encoding.ASCII.GetBytes(builder.Build().GetSection("JwtSettings").GetSection("Key").Value);
            var expiration = builder.Build().GetSection("JwtSettings").GetSection("Expiration").Value;

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.SLogin),
                    new Claim(ClaimTypes.Sid, user.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(Int32.Parse(expiration)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}

