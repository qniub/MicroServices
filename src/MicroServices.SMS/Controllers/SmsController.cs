using MicroServices.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroServices.SMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmsController : ControllerBase
    {
        [HttpPost(nameof(Send_MI))]
        public string Send_MI(SmsRequest model)
        {
            return this.GetMessage("小米", model);
        }

        [HttpPost(nameof(Send_LX))]
        public string Send_LX(SmsRequest model)
        {
            return this.GetMessage("联想", model);
        }

        [HttpPost(nameof(Send_HW))]
        public string Send_HW(SmsRequest model)
        {
            return this.GetMessage("华为", model);
        }

        private string GetMessage(string name, SmsRequest model)
        {
            return $"[{IPUtils.GetLocalIp()}]" +
                $"通过【{name}】短信接口向【{model.PhoneNum}】发送短信【{model.Msg}】";
        }
    }
}
