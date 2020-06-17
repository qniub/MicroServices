using System.Threading.Tasks;
using MicroServices.Models;
using WebApiClientCore.Attributes;

namespace MicroServices.Web.Api
{
    public interface ISmsApi
    {
        [HttpPost("/api/sms/" + nameof(Send_MI))]
        Task<string> Send_MI([JsonContent] SmsRequest model);

        [HttpPost("/api/sms/" + nameof(Send_LX))]
        Task<string> Send_LX([JsonContent] SmsRequest model);

        [HttpPost("/api/sms/" + nameof(Send_HW))]
        Task<string> Send_HW([JsonContent] SmsRequest model);
    }
}
