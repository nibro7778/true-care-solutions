using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Clients.Application.Commands;
using Microsoft.AspNetCore.Mvc;
using Clients.Application.Queries;

namespace Clients.Api;

public static class ClientsEndpoints
{
    private static readonly ClientsModule ClientModule = new();

    public static void UseClientsEndpoints(this WebApplication app)
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

        app.MapGet(path + "{clientId}", async ([FromRoute] Guid clientId, CancellationToken token) =>
        {
            var result = await ClientModule.SendQuery(new GetClient.Query(clientId), token);
            return Results.Ok(result);
        })
            .WithName($"{root} - GetClient")
        .WithOpenApi();
    }
}