namespace Host.IntegrationTests.Fixtures;

public class BaseTest
{
    protected readonly HttpClient Client;
    protected readonly TestFixture Service;

    protected BaseTest(TestFixture service)
    {
        Service = service;
        Client = service.CreateDefaultClient();        
    }
}