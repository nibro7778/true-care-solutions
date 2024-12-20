using Clients;
using Clients.Api;
using Microsoft.Extensions.Options;
using Staffs;
using Staffs.Api;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddLogging(x => x.AddSimpleConsole(opt => opt.SingleLine = true));

        // configuration to support API documentation and tooling. Specifically,
        // it registers the ability to generate OpenAPI/Swagger documentation for minimal APIs
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(type =>
            {
                return type.FullName!.Replace("+", ".");
            });
        });

        var module = typeof(ClientsModule).Assembly;

        // modules
        builder.Services.AddSingleton<IClients, ClientsModule>();
        builder.Services.AddSingleton<ClientsModuleStartup>();
        builder.Services.AddSingleton<IStaffs, StaffsModule>();
        builder.Services.AddSingleton<StaffsModuleStartup>();

        // ui
        builder.Services
            .AddRazorPages()
            .AddApplicationPart(module)
            .AddRazorRuntimeCompilation(c => c.FileProviders.Add(new EmbeddedFileProvider(module)));

        builder.Services.Configure<MvcRazorRuntimeCompilationOptions>(options =>
        {
            options.FileProviders.Add(new EmbeddedFileProvider(module));
        });

        // queue
        //builder.Services
        //    .AddHostedService<QueueJob>()
        //    .AddSingleton<QueueProcessor>()
        //    .AddSingleton<QueueRepository>()
        //    .AddSingleton<EventPublisher>();

        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseStaticFiles();
        app.UseRouting();
        // app.UseAuthentication();
        // app.UseAuthorization();
        app.UseClientsEndpoints();
        app.UseStaffsEndpoints();


        app.MapGet("/health/alive", () => "alive");
        app.MapGet("/health/ready", () => "ready");
        app.MapRazorPages();

        // module endpoints
        app.Services.GetRequiredService<ClientsModuleStartup>().Startup();
        app.Services.GetRequiredService<StaffsModuleStartup>().Startup();


        app.Run();
    }
}