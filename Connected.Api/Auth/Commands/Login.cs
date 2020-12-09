using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Connected.Api.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Connected.Api.Auth.Commands
{
    public class Login : IRequest<object>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginHandler : IRequestHandler<Login, object>
    {
        private readonly ConnectedContext _context;
        private readonly string _key;

        public LoginHandler(ConnectedContext context, IConfiguration configuration)
        {
            _context = context;
            _key = configuration["Key"];
        }

        public async Task<object> Handle(Login request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Username))
            {
                throw new ApplicationException();
            }

            var username = request.Username.ToLower().Trim();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == username,
                cancellationToken);

            if (user is null)
            {
                throw new ApplicationException();
            }

            if (request.Password != user.Password)
            {
                throw new ApplicationException("Could not login with provided credentials");
            }

            var token = CreateToken(user.Username);
            return token;
        }

        private string CreateToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "connected",
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_key)),
                        SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var wt = tokenHandler.WriteToken(token);
            return wt;
        }
    }
}