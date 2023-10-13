using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using CloudCustomers.UnitTests.Fixtures;
using CloudCustomers.UnitTests.Helpers;
using FluentAssertions;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudCustomers.UnitTests.Systems.Services
{
    public class TestUsersService
    {
        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokestHttpGetRequest()
        {
            // Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetRessourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);
            var sut = new UsersService(httpClient);

            // Act
            await sut.GetAllUsers();

            // Assert
            // Verify HTTP request is made !
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
            var expectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetRessourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);
            var sut = new UsersService(httpClient);

            // Act
            var result = await sut.GetAllUsers();

            // Assert
            result.Should().BeOfType<List<User>>();
        }


    }
}

