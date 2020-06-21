using System.Text;
using System.Threading.Tasks;
using MicroServices.Models;
using MicroServices.Web.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MicroServices.Web.Pages.Request
{
    [BindProperties]
    [IgnoreAntiforgeryToken]
    public class IndexModel : PageModel
    {
        public SmsInfo SmsRequest { get; set; }
        public EmailInfo EmailRequest { get; set; }

        public async Task<IActionResult> OnPostEmailAsync([FromServices] IEmailApi api)
        {
            string result = await api.Send_QQ(EmailRequest);

            return Message(result);
        }

        public async Task<IActionResult> OnPostSmsAsync([FromServices] ISmsApi api)
        {
            string result = await api.Send_MI(SmsRequest);

            return Message(result);
        }

        private IActionResult Message(string message)
        {
            return Content(
                $"<script>alert('{message}');location.href='/request/index';</script>",
                "text/html",
                Encoding.UTF8);
        }
    }
}
