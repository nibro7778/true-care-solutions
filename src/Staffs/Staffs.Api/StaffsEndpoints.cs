using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Staffs.Application.Queries;

namespace Staffs.Api;

public static class StaffsEndpoints
{
    private static readonly StaffsModule ModuleModule = new();
    
    public static void UseModuleEndpoints(this WebApplication app)
    {
        var root = StaffsApiAssemblyInfo.Assembly.GetName().Name;
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