using Microsoft.Extensions.Configuration;

namespace Common.Infrastructure.Configuration;

public static class DbConfigurationExtensions
{
    public static string GetDbConnectionString(this IConfiguration configuration, string schema)
    {    
        var username = configuration["DB_USERNAME"] ?? "sa";
        var password = configuration["DB_PASSWORD"] ?? "Admin1234!";
        var host = configuration["DB_HOST"] ?? "localhost";
        var port = configuration["DB_PORT"] ?? "1433";
        var connectionString = $"Server={host},{port};Database={schema};User Id={username};Password={password};TrustServerCertificate=True";
        return connectionString;
    }
}