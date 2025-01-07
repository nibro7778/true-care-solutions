using Microsoft.Data.SqlClient;

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
        // Set environment variables for the test connection
        string connectionString = "Server=localhost,1433;Database=TestDb;User Id=sa;Password=Admin1234!;TrustServerCertificate=True";
        Environment.SetEnvironmentVariable("ConnectionStrings:Clients", connectionString);
        Environment.SetEnvironmentVariable("ConnectionStrings:Staffs", connectionString);

        // Create TestDb
        await EnsureTestDatabaseCreatedAsync();
    }

    private async Task EnsureTestDatabaseCreatedAsync()
    {
        var masterConnectionString = "Server=localhost,1433;Database=master;User Id=sa;Password=Admin1234!;TrustServerCertificate=True";
        using var connection = new SqlConnection(masterConnectionString);
        await connection.OpenAsync();

        using var command = connection.CreateCommand();
        command.CommandText = "IF DB_ID('TestDb') IS NULL CREATE DATABASE TestDb;";
        await command.ExecuteNonQueryAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        //builder.ConfigureAppConfiguration(c => c.AddEnvironmentVariables());
        builder.ConfigureLogging(p => p.AddConsole());
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        // Cleanup environment variable
        Environment.SetEnvironmentVariable("ConnectionStrings:Clients", null);
        Environment.SetEnvironmentVariable("ConnectionStrings:Staffs", null);

        // Delete TestDb
        await EnsureTestDatabaseDeletedAsync();
    }

    private async Task EnsureTestDatabaseDeletedAsync()
    {
        var masterConnectionString = "Server=localhost,1433;Database=master;User Id=sa;Password=Admin1234!;TrustServerCertificate=True";
        using var connection = new SqlConnection(masterConnectionString);
        await connection.OpenAsync();

        using var command = connection.CreateCommand();
        command.CommandText = @"
            IF DB_ID('TestDb') IS NOT NULL 
            BEGIN
                ALTER DATABASE TestDb SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                DROP DATABASE TestDb;
            END";
        await command.ExecuteNonQueryAsync();
    }
}