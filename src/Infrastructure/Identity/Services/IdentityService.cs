using Application.Common.Interfaces;

namespace Infrastructure.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        public Task<bool> AuthorizeAsync(string userId, string policyName)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserNameAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsInRoleAsync(string userId, string role)
        {
            throw new NotImplementedException();
        }
    }
}
