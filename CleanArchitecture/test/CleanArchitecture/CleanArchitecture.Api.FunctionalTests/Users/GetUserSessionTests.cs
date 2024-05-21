using CleanArchitecture.Api.FunctionalTests.Infrastructure;
using CleanArchitecture.Application.Users.GetUserSession;
using CleanArchitecture.Application.Users.LoginUser;
using CleanArchitecture.Application.Users.RegisterUsers;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Xunit;

namespace CleanArchitecture.Api.FunctionalTests.Users;

public class GetUserSessionTests : BaseFunctionalTest
{
    public GetUserSessionTests(FunctionalTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Get_Should_ReturnUnauthorized_When_TokenIsMissing()
    {
        //Act
        var response = await HttpClient.GetAsync("api/v1/users/me");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Get_Should_ReturnUser_When_TokenExists()
    {
        //Arrange
        var token = await GetToken();
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            JwtBearerDefaults.AuthenticationScheme, token);

        //Act
        var user = await HttpClient.GetFromJsonAsync<UserResponse>("api/v1/users/me");

        //Assert
        user.Should().NotBeNull();
    }

    [Fact]
    public async Task Login_Should_ReturnOK_When_UserExists()
    {
        //Arrange
        var request = new LoginUserRequest(UserData.RegisterUserRequestTest.Email, UserData.RegisterUserRequestTest.Password);

        //Act
        var response = await HttpClient.PostAsJsonAsync("api/v1/users/login", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Register_Should_ReturnOK_When_RequestIsValid()
    {
        //Arrange
        var request = new RegisterUserRequest("testx@test.com", "test1", "test2", "Test11233##");

        //Act
        var response = await HttpClient.PostAsJsonAsync("api/v1/users/register", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
