using System.Reflection;
using Common;
using Common.Infrastructure.Configuration;
using Common.Infrastructure.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Staffs.Application;
using Staffs.Infrastructure;

namespace Staffs;

public class StaffsModuleStartup : IModuleStartup
{
    private readonly IConfiguration _configuration;
    private readonly ILoggerProvider _logs;

    public StaffsModuleStartup(IConfiguration configuration, ILoggerProvider logs)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        ArgumentNullException.ThrowIfNull(logs);

        _configuration = configuration;
        _logs = logs;
    }

    public void Startup()
    {
        var connectionString = _configuration.GetDbConnectionString(Schema);
        var assembly = Assembly.GetExecutingAssembly();

        var assemblies = new[]
        {
            StaffsAssemblyInfo.Assembly,
            CommonAssemblyInfo.Assembly
        };

        var provider = new ServiceCollection()
            .AddCommon()
            // entrypoint
            .AddSingleton<IStaffs, StaffsModule>()
            // application
            .AddMediatR(c => c.RegisterServicesFromAssemblies(assemblies))
            .AddValidatorsFromAssemblies(assemblies)
            // infrastructure
            .AddScoped<IDbConnectionFactory, DbConnectionFactory>(serviceProvider =>
            {
                var log = serviceProvider.GetRequiredService<ILogger<DbConnectionFactory>>();
                return new DbConnectionFactory(connectionString, log);
            })
            .AddScoped<IWidgetRepository, WidgetRepository>()
            // logging
            .AddLogging(c => { c.AddProvider(_logs).AddSimpleConsole(c => c.SingleLine = true); })
            // builder container
            .BuildServiceProvider();

        CompositionRoot.SetProvider(provider);

        DbMigrations.Apply(Schema, connectionString, assembly, reset: false);

        DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    public void Destroy()
    {
        var connectionString = _configuration.GetDbConnectionString(Schema);
        var assembly = Assembly.GetExecutingAssembly();

        DbMigrations.Apply(Schema, connectionString, assembly, reset: false);
    }
}