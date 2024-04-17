using CleanArchitecture.Domain.Users;

namespace CleanArchitecture.Application.Abstractions.Authentication;

public interface IJwtProvider
{
    Task<string> Generate(User user);
}
