using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Clients.Application.Queries;

namespace Clients.Api;

public static class ClientsEndpoints
{
    private static readonly ClientsModule ModuleModule = new();
    
    public static void UseModuleEndpoints(this WebApplication app)
    {
        var root = ClientsApiAssemblyInfo.Assembly.GetName().Name;
        var path = $"/{root}/hello";
        app.MapGet(path, async (CancellationToken token) =>
            {
                return Results.Ok("hello");
            })
            .WithName($"{root} - GetClient ")
            .WithOpenApi();
    }
}