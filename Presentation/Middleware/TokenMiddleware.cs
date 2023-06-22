using Infrastructure.Interface.Repository.User;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Presentation.Middleware
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public TokenMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;

        }

        public async Task Invoke(HttpContext context, IUserQueryRepository userRepository)
        {
            string? token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                AttachUserToContextAsync(context, token, userRepository);
            }

            await _next(context);
        }


        private void AttachUserToContextAsync(HttpContext context, string token, IUserQueryRepository userRepository)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

                byte[] key = Encoding.ASCII.GetBytes(_configuration["Tokens:SecretKey"]!);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),

                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Tokens:Issuer"],

                    ValidateAudience = true,
                    ValidAudience = _configuration["Tokens:Audience"],

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero

                }, out SecurityToken validatedToken);

                JwtSecurityToken jwtToken = (JwtSecurityToken)validatedToken;
                string username = (jwtToken.Claims.First(x => x.Type == "Email").Value);

                //validate service!!!

                // attach user to context on successful jwt validation

                context.Items["Email"] = username;
                context.Items["User"] = userRepository.GetUserByEmail(username);
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}
