using Application.Common.Interfaces;
using Application.Common.Models;
using BCrypt.Net;
using Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BC = BCrypt.Net.BCrypt;

namespace Infrastructure.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbContext _context;
        private readonly AppSettings _appSettings;
        public UserService(IApplicationDbContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == model.Username);

            // return null if user not found
            if (user == null || !BC.EnhancedVerify(model.Password, user.Password, HashType.SHA512)) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(Guid id)
        {
            return _context.Users.FirstOrDefault(a => a.Id == id && a.DeletedTime == null);
        }

        public Task<string> GetUserNameAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsInRoleAsync(Guid userId, string role)
        {
            throw new NotImplementedException();
        }

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()), new Claim("role", "admin") }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
