using System.Collections.Generic;
using System.Threading.Tasks;
using MicroServices.Models;
using MicroServices.Web.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MicroServices.Web.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly IOrdersApi _api;

        public IndexModel(IOrdersApi api)
        {
            _api = api;
        }

        public List<Order> Orders { get; set; }

        public async Task OnGetAsync()
        {
            Orders = await _api.Get();
        }

        public async Task<IActionResult> OnGetDeleteAsync(long id)
        {
            await _api.Delete(id);

            return this.RedirectToPage("./Index");
        }
    }
}
