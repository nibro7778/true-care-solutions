using Common;
using Host.Infrastructure.Integration;
using Clients;
using Clients.Api;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLogging(x => x.AddSimpleConsole(opt => opt.SingleLine = true));
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
builder.Services
    .AddHostedService<QueueJob>()
    .AddSingleton<QueueProcessor>()
    .AddSingleton<QueueRepository>()
    .AddSingleton<EventPublisher>();

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