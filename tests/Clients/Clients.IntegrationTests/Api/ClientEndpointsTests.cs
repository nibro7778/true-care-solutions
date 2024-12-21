﻿using System.Net.Http.Json;
using Clients.Application.Commands;
using FluentAssertions;
using Host.IntegrationTests.Fixtures;
using Xunit.Abstractions;

namespace Clients.IntegrationTests.Api
{
    public class ClientEndpointsTests : BaseTest, IClassFixture<TestFixture>
    {             
        public ClientEndpointsTests(TestFixture service, ITestOutputHelper output) : base(service, output)
        {
        }
    
        [Fact]
        public async Task AddClient_ShouldReturnCreated()
        {
            // Arrange
            var command = new CreateClient.Command(Guid.NewGuid(), "Alex", "Minati");

            // Act
            var response = await Client.PostAsJsonAsync("/Clients.Api/", command);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }
    }
}