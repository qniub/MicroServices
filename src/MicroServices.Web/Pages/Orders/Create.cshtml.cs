using System.Threading.Tasks;
using MicroServices.Models;
using MicroServices.Web.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MicroServices.Web.Pages.Orders
{
    [IgnoreAntiforgeryToken]
    public class CreateModel : PageModel
    {
        private readonly IOrdersApi _api;

        public CreateModel(IOrdersApi api)
        {
            _api = api;
        }

        [BindProperty]
        public Order Order { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _api.Create(this.Order);

            return RedirectToPage("./Index");
        }
    }
}
