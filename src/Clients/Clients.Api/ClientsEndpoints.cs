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
        var path = $"/{root}/widgets";
        app.MapGet(path, async (CancellationToken token) =>
            {
                var results = await ModuleModule.SendQuery(new ListWidgets.Query(),token);
                return Results.Ok(results);
            })
            .WithName($"{root} - GetWidgets ")
            .WithOpenApi();
    }
}