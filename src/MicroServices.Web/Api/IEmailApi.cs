using System.Threading.Tasks;
using MicroServices.Models;
using WebApiClientCore.Attributes;

namespace MicroServices.Web.Api
{
    public interface IEmailApi
    {
        [HttpPost("/api/email/" + nameof(Send_QQ))]
        Task<string> Send_QQ([JsonContent] EmailInfo model);

        [HttpPost("/api/email/" + nameof(Send_Outlook))]
        Task<string> Send_Outlook([JsonContent] EmailInfo model);

        [HttpPost("/api/email/" + nameof(Send_Google))]
        Task<string> Send_Google([JsonContent] EmailInfo model);
    }
}
