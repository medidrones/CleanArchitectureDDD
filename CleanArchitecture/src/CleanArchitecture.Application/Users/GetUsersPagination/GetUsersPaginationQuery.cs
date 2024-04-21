using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Users;

namespace CleanArchitecture.Application.Users.GetUsersPagination;

public record GetUsersPaginationQuery : PaginationParams, IQuery<PagedResults<User, UserId>>
{
}
