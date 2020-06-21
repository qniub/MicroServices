using System.Threading.Tasks;
using MicroServices.Models;
using MicroServices.Web.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MicroServices.Web.Pages.Orders
{
    [IgnoreAntiforgeryToken]
    public class EditModel : PageModel
    {
        private readonly IOrdersApi _api;

        public EditModel(IOrdersApi api)
        {
            _api = api;
        }

        [BindProperty]
        public OrderInfo Order { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order = await _api.GetById(id.Value);

            if (Order == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _api.Edit(Order.Id, Order);

            return RedirectToPage("./Index");
        }
    }
}
