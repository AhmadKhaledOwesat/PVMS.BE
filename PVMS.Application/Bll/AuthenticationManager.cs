using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using PVMS.Application.Interfaces;
using PVMS.Application.Dto;

namespace PVMS.Application.Bll
{
    public class AuthenticationManager(IConfiguration configuration) : IAuthenticationManager
    {
        public Tokens GenerateToken(string userName, object id)
        {
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
            var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]!);
            SecurityTokenDescriptor securityTokenDescriptor = new()
            {
                Subject = new(new[] { new(ClaimTypes.Name, userName), new Claim(ClaimTypes.UserData, $"{id}") }),
                Expires = DateTime.UtcNow.AddMonths(10),//TODO :: Should be handle , just for test
                SigningCredentials = new(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            return new Tokens { Token = jwtSecurityTokenHandler.WriteToken(token) };
        }
    }
}
