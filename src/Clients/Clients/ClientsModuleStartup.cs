using System.Reflection;
using Common;
using Common.Infrastructure.Configuration;
using Common.Infrastructure.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Clients.Application;
using Clients.Infrastructure;

namespace Clients;

public class ClientsModuleStartup : IModuleStartup
{
    private readonly IConfiguration _configuration;
    private readonly ILoggerProvider _logs;

    public ClientsModuleStartup(IConfiguration configuration, ILoggerProvider logs)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        ArgumentNullException.ThrowIfNull(logs);

        _configuration = configuration;
        _logs = logs;
    }

    public void Startup()
    {
        var connectionString = _configuration.GetDbConnectionString(Database);
        var assembly = Assembly.GetExecutingAssembly();

        var assemblies = new[]
        {
            ClientsAssemblyInfo.Assembly,
            CommonAssemblyInfo.Assembly
        };

        var provider = new ServiceCollection()
            .AddCommon()
            // entrypoint
            .AddSingleton<IClients, ClientsModule>()
            // application
            .AddMediatR(c => c.RegisterServicesFromAssemblies(assemblies))
            .AddValidatorsFromAssemblies(assemblies)
            // infrastructure
            .AddScoped<IDbConnectionFactory, DbConnectionFactory>(serviceProvider =>
            {
                var log = serviceProvider.GetRequiredService<ILogger<DbConnectionFactory>>();
                return new DbConnectionFactory(connectionString, log);
            })
            .AddScoped<IClientRepository, ClientRepository>()
            // logging
            .AddLogging(c => { c.AddProvider(_logs).AddSimpleConsole(c => c.SingleLine = true); })
            // builder container
            .BuildServiceProvider();

        CompositionRoot.SetProvider(provider);

        DbMigrations.Apply(Database, connectionString, assembly, reset: false);

        DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    public void Destroy()
    {
        var connectionString = _configuration.GetDbConnectionString(Database);
        var assembly = Assembly.GetExecutingAssembly();

        DbMigrations.Apply(Database, connectionString, assembly, reset: false);
    }
}