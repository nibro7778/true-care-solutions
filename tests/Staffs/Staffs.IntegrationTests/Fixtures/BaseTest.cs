using Xunit.Abstractions;

namespace Staffs.IntegrationTests.Fixtures;

[Collection(nameof(ServiceFixture))]
public class BaseTest
{
    protected BaseTest(ServiceFixture service, ITestOutputHelper output)
    {
        Service = service;
        service.OutputHelper = output;
        ModuleModule = new StaffsModule();
    }

    protected ServiceFixture Service { get; init; }
    
    protected StaffsModule ModuleModule { get; init; }
}