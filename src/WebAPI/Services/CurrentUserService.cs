using Application.Common.Interfaces;
using Domain.Entities;

namespace WebAPI.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId => ((User)_httpContextAccessor.HttpContext.Items["User"]).Id;
}
