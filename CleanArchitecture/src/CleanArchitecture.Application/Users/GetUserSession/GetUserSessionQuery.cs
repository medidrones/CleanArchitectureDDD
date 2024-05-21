using CleanArchitecture.Application.Abstractions.Messaging;

namespace CleanArchitecture.Application.Users.GetUserSession;

public sealed record GetUserSessionQuery : 
    IQuery<UserResponse>;
