using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Application.Paginations;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Users;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Users.GetUsersPagination;

internal sealed class GetUsersPaginationQueryHandler 
    : IQueryHandler<GetUsersPaginationQuery, PagedResults<User, UserId>>
{
    private readonly IPaginationRepository _paginationRepository;

    public GetUsersPaginationQueryHandler(IPaginationRepository paginationRepository)
    {
        _paginationRepository = paginationRepository;
    }

    public async Task<Result<PagedResults<User, UserId>>> Handle(GetUsersPaginationQuery request, CancellationToken cancellationToken)
    {
        var predicateb = PredicateBuilder.New<User>(true);

        if (!string.IsNullOrEmpty(request.Search))
        {
            predicateb = predicateb.Or(p => p.Nombre == new Nombre(request.Search));
            predicateb = predicateb.Or(p => p.Email == new Email(request.Search));
        }

        var pagedResultUsuarios = await _paginationRepository.GetPaginationAsync(
            predicateb, 
            p => p.Include(x => x.Roles!).ThenInclude(y => y.Permissions!),
            request.PageNumber, request.PageSize, request.OrderBy!, request.OrderAsc);

        return pagedResultUsuarios;
    }
}
