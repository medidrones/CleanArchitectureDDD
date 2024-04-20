using CleanArchitecture.Application.Abstractions.Authentication;
using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Domain.Users;
using Dapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchitecture.Infrastructure.Authentication;

public sealed class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _options;
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public JwtProvider(IOptions<JwtOptions> options, ISqlConnectionFactory sqlConnectionFactory)
    {
        _options = options.Value;
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<string> Generate(User user)
    {
        const string sql = """
              SELECT 
                p.nombre
              FROM users usr
                LEFT JOIN users_roles usrl
                    ON usr.id=usrl.user_id
                LEFT JOIN roles rl
                    ON rl.id=usrl.role_id
                LEFT JOIN roles_permissions rp
                    ON rl.id=rp.role_id
                LEFT JOIN permissions p
                    ON p.id=rp.permission_id
                WHERE usr.id=@UserId            
        """;

        using var connection = _sqlConnectionFactory.CreateConnection();

        var permissions = await connection.QueryAsync<string>(sql, new { UserId = user.Id!.Value });
        var permissionCollection = permissions.ToHashSet();

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id!.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!.Value)
        };

        foreach (var permission in permissionCollection)
        {
            claims.Add(new (CustomClaims.Permissions, permission));
        }

        var sigingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey!)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            claims,
            null,
            DateTime.UtcNow.AddDays(365),
            sigingCredentials);

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }
}
