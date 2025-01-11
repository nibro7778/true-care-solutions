namespace Host.IntegrationTests.Fixtures;

// This fixture manage SQL server container lifecycle and
// Use the WebApplicationFactory to customize the host configuration for integration tests
public class TestFixture : WebApplicationFactory<HostAssemblyInfo>, IAsyncLifetime
{
    public TestFixture()
    {        
    }

    public async Task InitializeAsync()
    {
         
    }

    

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        //builder.ConfigureAppConfiguration(c => c.AddEnvironmentVariables());
        builder.ConfigureLogging(p => p.AddConsole());
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        
    }    
}