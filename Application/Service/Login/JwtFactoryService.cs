using Core.Interface.Services.Login;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Service.Login
{
    public class JwtFactoryService : IJwtFactoryService
    {
        private readonly IConfiguration _configuration;

        public JwtFactoryService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TokenEntity GenerateToken(UserEntity user)
        {
            Claim[] claims = new[]
           {
                 new Claim("Name", user.UserName!),
                 new Claim("Email", user.Email!),
                 new Claim(ClaimTypes.Role, user.RoleId.ToString()),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:SecretKey"]!));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                _configuration["Tokens:Issuer"],
                _configuration["Tokens:Audience"],
                claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Tokens:ExpireMinutes"]!)),
                signingCredentials: credentials);
            var results = new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo.ToLocalTime()
            };
            return new TokenEntity(results.token, results.expiration);
        }
    }
}
