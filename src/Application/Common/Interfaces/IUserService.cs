using Application.Common.Models;
using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IUserService
{
    AuthenticateResponse Authenticate(AuthenticateRequest model);
    Task<User> GetUserAsync(Guid userId);
    Task<bool> IsInRoleAsync(Guid userId, string role);
    User GetById(Guid userId);
    Task<IEnumerable<User>> GetAllUsersAsync();
}
