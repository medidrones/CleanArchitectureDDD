using CleanArchitecture.Application.Abstractions.Authentication;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Infrastructure.Authentication;

internal sealed class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string UserEmail => _httpContextAccessor.HttpContext?.User.GetUserEmail() ??
        throw new ApplicationException("El user context es invalido");

    public Guid UserId => _httpContextAccessor.HttpContext?.User.GetUserId() ?? 
        throw new ApplicationException("El user context es invalido");
}
