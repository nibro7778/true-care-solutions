namespace Host.IntegrationTests.Fixtures;

public class BaseTest
{
    protected readonly HttpClient Client;
    protected readonly TestFixture Service;

    protected BaseTest(TestFixture service, ITestOutputHelper output)
    {
        Service = service;
        Service.OutputHelper = output;
        Client = service.CreateDefaultClient();
    }
}