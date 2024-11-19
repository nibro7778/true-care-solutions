using Xunit.Abstractions;

namespace Clients.IntegrationTests.Fixtures;

[Collection(nameof(ServiceFixture))]
public class BaseTest
{
    protected BaseTest(ServiceFixture service, ITestOutputHelper output)
    {
        Service = service;
        service.OutputHelper = output;
        ModuleModule = new ClientsModule();
    }

    protected ServiceFixture Service { get; init; }
    
    protected ClientsModule ModuleModule { get; init; }
}