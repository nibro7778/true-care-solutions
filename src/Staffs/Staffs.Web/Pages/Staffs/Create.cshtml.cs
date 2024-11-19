using Microsoft.AspNetCore.Mvc;
using Staffs.Application.Commands;

namespace Staffs.Web.Pages.Staffs;

public class Create(IStaffs module) : PageModel
{
    public async Task<RedirectToPageResult> OnPostAsync(string name, decimal price, CancellationToken cancellationToken)
    {
        var command = new CreateWidget.Command(Guid.NewGuid(),  name, price);
        await module.SendCommand(command, cancellationToken);
        return RedirectToPage(nameof(Index));
    }
}