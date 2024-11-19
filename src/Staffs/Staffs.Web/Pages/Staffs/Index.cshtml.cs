using static Staffs.Application.Queries.ListWidgets;

namespace Staffs.Web.Pages.Staffs
{
    public class IndexModel(IStaffs module) : PageModel
    {
        public async Task OnGet(CancellationToken cancellationToken)
        {
            Widgets = await module.SendQuery(new Query(),cancellationToken);
        }

        public IEnumerable<Response> Widgets { get; set; } = null!;
    }
}
