using Microsoft.Extensions.Configuration;
using Testcontainers.MsSql;

namespace Host.IntegrationTests.Fixtures;

public class TestFixture : WebApplicationFactory<HostAssemblyInfo>, ITestOutputHelperAccessor, IAsyncLifetime
{
    public ITestOutputHelper? OutputHelper { get; set; }
    private readonly MsSqlContainer _sqlContainer;

    public TestFixture()
    {
        _sqlContainer = new MsSqlBuilder()
            .WithPassword("Admin1234!")
            .Build();
    }

    public async Task InitializeAsync()
    {
        await _sqlContainer.StartAsync();

        // Set environment variables for the test connection
        Environment.SetEnvironmentVariable("DB_HOST", _sqlContainer.Hostname);
        Environment.SetEnvironmentVariable("DB_PORT", "1433");
        Environment.SetEnvironmentVariable("DB_USERNAME", "sa");
        Environment.SetEnvironmentVariable("DB_PASSWORD", "Admin1234!");
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(c => c.AddEnvironmentVariables());
        builder.ConfigureLogging(p => p.AddXUnit(this));
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _sqlContainer.StopAsync();

        // Clean up environment variables
        Environment.SetEnvironmentVariable("DB_HOST", null);
        Environment.SetEnvironmentVariable("DB_PORT", null);
        Environment.SetEnvironmentVariable("DB_USERNAME", null);
        Environment.SetEnvironmentVariable("DB_PASSWORD", null);
    }
}