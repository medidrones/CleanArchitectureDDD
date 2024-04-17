using CleanArchitecture.Application.Abstractions.Authentication;
using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Users;

namespace CleanArchitecture.Application.Users.LoginUser;

internal sealed class LoginCommandHandler : ICommandHandler<LoginCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;

    public LoginCommandHandler(IUserRepository userRepository, IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(new Email(request.Email), cancellationToken);

        if (user is null)
        {
            return Result.Failure<string>(UserErrors.NotFound);
        }

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash!.Value))
        {
            return Result.Failure<string>(UserErrors.InvalidCredentials);
        }

        var token = await _jwtProvider.Generate(user);

        return token;
    }
}
