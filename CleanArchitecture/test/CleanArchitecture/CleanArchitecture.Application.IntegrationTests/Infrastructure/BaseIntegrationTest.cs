using CleanArchitecture.Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CleanArchitecture.Application.IntegrationTests.Infrastructure;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
{
    private readonly IServiceScope _scope;
    protected readonly ISender sender;
    protected readonly ApplicationDbContext dbContext;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();
        sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        dbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    }
}
