using Clients;
using Clients.Api;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddLogging(x => x.AddSimpleConsole(opt => opt.SingleLine = true));

        // configuration to support API documentation and tooling. Specifically,
        // it registers the ability to generate OpenAPI/Swagger documentation for minimal APIs
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var module = typeof(ClientsModule).Assembly;

        // modules
        builder.Services.AddSingleton<IClients, ClientsModule>();
        builder.Services.AddSingleton<ClientsModuleStartup>();

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
        app.UseModuleEndpoints();

        app.MapGet("/meta/name", () => Assembly.GetExecutingAssembly().GetName());
        app.MapGet("/health/alive", () => "alive");
        app.MapGet("/health/ready", () => "ready");
        app.MapRazorPages();

        // module endpoints
        app.Services.GetRequiredService<ClientsModuleStartup>().Startup();

        app.Run();
    }
}