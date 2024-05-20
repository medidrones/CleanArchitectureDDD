using CleanArchitecture.Application.IntegrationTests.Infrastructure;
using CleanArchitecture.Application.Vehiculos.SearchVehiculos;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.Application.IntegrationTests.Vehiculos;

public class SearchVehiculosTests : BaseIntegrationTest
{
    public SearchVehiculosTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task SearchVehiculos_Should_ReturnEmptyList_When_DateRangeInvalid()
    {
        //Arrange
        var query = new SearchVehiculosQuery(new DateOnly(2023,1,1), new DateOnly(2022,1,1));

        //Act
        var resultado = await sender.Send(query);

        //Assert
        resultado.Value.Should().BeEmpty();
    }

    [Fact]
    public async Task SearchVehiculos_Should_ReturnVehiculos_When_DateRangeIsValid()
    {
        //Arrange
        var query = new SearchVehiculosQuery(new DateOnly(2023, 1, 1), new DateOnly(2026, 1, 1));

        //Act
        var resultado = await sender.Send(query);

        //Assert
        resultado.IsSuccess.Should().BeTrue();
    }
}
