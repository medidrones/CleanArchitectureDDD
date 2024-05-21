namespace CleanArchitecture.Application.Abstractions.Authentication;

public interface IUserContext
{
    string UserEmail { get; }
    Guid UserId { get; }
}
