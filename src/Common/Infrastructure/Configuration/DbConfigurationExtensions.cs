using Microsoft.Extensions.Configuration;

namespace Common.Infrastructure.Configuration;

public static class DbConfigurationExtensions
{
    public static string GetDbConnectionString(this IConfiguration configuration, string schema)
    {
        //TODO: Update following
        //var username = configuration["DB_USERNAME"] ?? "sa";
        //var password = configuration["DB_PASSWORD"] ?? "Admin1234!";
        //var database = configuration["DB_DATABASE"] ?? "clie";
        //var host = configuration["DB_HOST"] ?? "localhost";
        //var port = configuration["DB_PORT"] ?? "1433";
        var connectionString = $"Server=localhost,1433;Database=clients_module;User Id=sa;Password=Admin1234!;TrustServerCertificate=True";
        return connectionString;
    }
}