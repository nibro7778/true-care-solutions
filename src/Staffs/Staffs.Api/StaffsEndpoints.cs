using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Staffs.Application.Commands;
using Staffs.Application.Queries;

namespace Staffs.Api;

public static class StaffsEndpoints
{
    private static readonly StaffsModule StafftModule = new();

    public static void UseStaffsEndpoints(this WebApplication app)
    {
        var root = StaffsApiAssemblyInfo.Assembly.GetName().Name;
        var path = $"/{root}/";

        app.MapPost(path, async ([FromBody] CreateStaff.Command request, CancellationToken token) =>
        {
            await StafftModule.SendCommand(request, token);
            return Results.Created();
        })
            .WithName($"{root} - AddStaff")
        .WithOpenApi();

        app.MapGet(path + "{staffId}", async ([FromRoute] Guid staffId, CancellationToken token) =>
        {
            var result = await StafftModule.SendQuery(new GetStaff.Query(staffId), token);
            return Results.Ok(result);
        })
            .WithName($"{root} - GetStaff")
        .WithOpenApi();
    }
}