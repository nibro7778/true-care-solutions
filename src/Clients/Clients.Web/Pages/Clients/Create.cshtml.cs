using Microsoft.AspNetCore.Mvc;
using Clients.Application.Commands;

namespace Clients.Web.Pages.Clients;

public class Create(IClients module) : PageModel
{
    public async Task<RedirectToPageResult> OnPostAsync(string name, decimal price, CancellationToken cancellationToken)
    {
        var command = new CreateWidget.Command(Guid.NewGuid(),  name, price);
        await module.SendCommand(command, cancellationToken);
        return RedirectToPage(nameof(Index));
    }
}