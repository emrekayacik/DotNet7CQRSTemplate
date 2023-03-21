using Application.Common.Models;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(Guid id);
        Task<string> GetUserNameAsync(Guid userId);
        Task<bool> IsInRoleAsync(Guid userId, string role);
    }
}
