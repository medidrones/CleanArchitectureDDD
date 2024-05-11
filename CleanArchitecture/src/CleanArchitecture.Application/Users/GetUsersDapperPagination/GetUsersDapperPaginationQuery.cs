using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Shared;

namespace CleanArchitecture.Application.Users.GetUsersDapperPagination;

public sealed record GetUsersDapperPaginationQuery 
    : PaginationParams, IQuery<PagedDapperResults<UserPaginationData>>;
