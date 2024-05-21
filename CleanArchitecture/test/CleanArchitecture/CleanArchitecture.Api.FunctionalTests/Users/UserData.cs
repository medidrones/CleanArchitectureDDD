using CleanArchitecture.Application.Users.RegisterUsers;

namespace CleanArchitecture.Api.FunctionalTests.Users;

internal static class UserData
{
    public static RegisterUserRequest RegisterUserRequestTest = 
        new("felipe@test.com", "Felipe", "Rosas", "Test123$");
}
