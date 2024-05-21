using CleanArchitecture.Api.FunctionalTests.Users;
using CleanArchitecture.Application.Users.LoginUser;
using System.Net.Http.Json;
using Xunit;

namespace CleanArchitecture.Api.FunctionalTests.Infrastructure;

public abstract class BaseFunctionalTest : IClassFixture<FunctionalTestWebAppFactory>
{
    protected readonly HttpClient HttpClient;

    protected BaseFunctionalTest(FunctionalTestWebAppFactory factory)
    {
        HttpClient = factory.CreateClient();
    }

    protected async Task<string> GetToken()
    {
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("api/v1/users/login", 
            new LoginUserRequest(UserData.RegisterUserRequestTest.Email, UserData.RegisterUserRequestTest.Password));

        return await response.Content.ReadAsStringAsync();
    }
}
