using CleanArchitecture.Application.Abstractions.Authentication;
using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using Dapper;

namespace CleanArchitecture.Application.Users.GetUserSession;

internal sealed class GetUserSessionQueryHandler : IQueryHandler<GetUserSessionQuery, UserResponse>
{
    private readonly IUserContext _userContext;
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetUserSessionQueryHandler(IUserContext userContext, ISqlConnectionFactory sqlConnectionFactory)
    {
        _userContext = userContext;
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<UserResponse>> Handle(GetUserSessionQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();
        const string sql = """
            SELECT
                id as Id,
                nombre as Nombre,
                apellido as Apellido,
                email as Email
            FROM users
            WHERE id = @id
        """;

        var user = await connection.QuerySingleAsync<UserResponse>(sql, new { id = _userContext.UserId });

        return user;
    }
}
