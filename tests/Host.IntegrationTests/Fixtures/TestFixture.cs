using Microsoft.Extensions.Configuration;
using Testcontainers.MsSql;

namespace Host.IntegrationTests.Fixtures;

// This fixture manage SQL server container lifecycle and
// Use the WebApplicationFactory to customize the host configuration for integration tests
public class TestFixture : WebApplicationFactory<HostAssemblyInfo>, ITestOutputHelperAccessor, IAsyncLifetime
{
    public ITestOutputHelper? OutputHelper { get; set; }
    private readonly MsSqlContainer _sqlContainer;

    public TestFixture()
    {
        _sqlContainer = new MsSqlBuilder()        
            .WithPortBinding(1500, true)
            .WithAutoRemove(true)
            .Build();
    }

    public async Task InitializeAsync()
    {
        await _sqlContainer.StartAsync();

       // Set environment variables for the test connection
        Environment.SetEnvironmentVariable("ConnectionStrings:Clients", _sqlContainer.GetConnectionString());
        Environment.SetEnvironmentVariable("ConnectionStrings:Staffs", _sqlContainer.GetConnectionString());
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(c => c.AddEnvironmentVariables());
        builder.ConfigureLogging(p => p.AddXUnit(this));
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _sqlContainer.StopAsync();

        // Cleanup environment variable
        Environment.SetEnvironmentVariable("ConnectionStrings:Clients", null);
        Environment.SetEnvironmentVariable("ConnectionStrings:Staffs", null);
    }
}