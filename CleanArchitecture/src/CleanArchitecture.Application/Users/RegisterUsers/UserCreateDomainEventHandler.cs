using CleanArchitecture.Application.Abstractions.Email;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Users.Events;
using MediatR;

namespace CleanArchitecture.Application.Users.RegisterUsers;

internal sealed class UserCreateDomainEventHandler : INotificationHandler<UserCreateDomainEvent>
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;

    public UserCreateDomainEventHandler(IUserRepository userRepository, IEmailService emailService)
    {
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task Handle(UserCreateDomainEvent notification, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(notification.UserId, cancellationToken);
        
        if (user is null)
        {
            return;
        }

        _emailService.Send(
            user.Email!.Value!,
            "Se ha creado su cuenta en nuestra App",
            "Tienes una nueva cuenta dentro de Clean Architecture"
            );
    }
}
