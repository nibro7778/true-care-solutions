using static Clients.Application.Queries.ListWidgets;

namespace Clients.Web.Pages.Clients
{
    public class IndexModel(IClients module) : PageModel
    {
        public async Task OnGet(CancellationToken cancellationToken)
        {
            Widgets = await module.SendQuery(new Query(),cancellationToken);
        }

        public IEnumerable<Response> Widgets { get; set; } = null!;
    }
}
