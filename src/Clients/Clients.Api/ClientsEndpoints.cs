using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Clients.Application.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Clients.Api;

public static class ClientsEndpoints
{
    private static readonly ClientsModule ClientModule = new();

    public static void UseModuleEndpoints(this WebApplication app)
    {
        var root = ClientsApiAssemblyInfo.Assembly.GetName().Name;
        var path = $"/{root}/";
        app.MapPost(path, async ([FromBody] CreateClient.Command request, CancellationToken token) =>
            {
                await ClientModule.SendCommand(request, token);
                return Results.Created();
            })
            .WithName($"{root} - AddClient")
        .WithOpenApi();        
    }
}