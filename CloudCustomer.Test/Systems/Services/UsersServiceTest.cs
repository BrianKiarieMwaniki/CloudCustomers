using CloudCustomer.API.Config;
using CloudCustomer.Test.Fixtures;
using CloudCustomer.Test.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq.Protected;

namespace CloudCustomer.Test.Systems.Services;

public class UsersServiceTest
{
    [Fact]
    public async Task GetAllUsers_WhenCalled_InvokesHttpGetRequest()
    {
        // Arrange
        var expectedResponse = UsersFixture.GetTestUsers();
        var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
        var httpClient = new HttpClient(handlerMock.Object);

        var endpoint = "https://foo.com";
        var config = Options.Create(
            new UsersApiOptions { Endpoint = endpoint });

        var sut = new UsersService(httpClient, config);

        // Act
        await sut.GetAllUsers();

        // Assert
        handlerMock
            .Protected()
            .Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
                );
    }

    [Fact]
    public async Task GetAllUsers_WhenCalled_ReturnsListOfUsers()
    {
        // Arrange
        var handlerMock = MockHttpMessageHandler<User>.SetupReturn404();
        var httpClient = new HttpClient(handlerMock.Object);

        var endpoint = "https://foo.com";
        var config = Options.Create(
            new UsersApiOptions { Endpoint = endpoint });

        var sut = new UsersService(httpClient, config);

        // Act
        var result = await sut.GetAllUsers();

        // Assert
        result.Should().BeEmpty();
        _ = result?.Count.Should().Be(0);
    }

    [Fact]
    public async Task GetAllUsers_WhenCalled_ReturnListOfUserOfExpectedSize()
    {
        // Arrange
        var expectedResponse = UsersFixture.GetTestUsers();
        var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
        var httpClient = new HttpClient(handlerMock.Object);

        var endpoint = "https://foo.com";
        var config = Options.Create(
            new UsersApiOptions { Endpoint = endpoint });

        var sut = new UsersService(httpClient, config);

        // Act
        var result = await sut.GetAllUsers();

        // Assert
        result.Should().NotBeNullOrEmpty();
        _ = result?.Count.Should().Be(expectedResponse.Count);
    }

    [Fact]
    public async Task GetAllUsers_WhenCalled_InvokesConfiguredExternalUrl()
    {
        // Arrange
        var expectedResponse = UsersFixture.GetTestUsers();
        var endpoint = "https://foo.com";
        var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse, endpoint);
        var httpClient = new HttpClient(handlerMock.Object);

        var config = Options.Create(new UsersApiOptions
        {
            Endpoint = endpoint
        });

        var sut = new UsersService(httpClient, config);

        // Act
        var result = await sut.GetAllUsers();

        // Assert
        handlerMock
           .Protected()
           .Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(
                   req => req.Method == HttpMethod.Get && 
                   req.RequestUri.ToString() == endpoint),
               ItExpr.IsAny<CancellationToken>()
               );
    }
}
