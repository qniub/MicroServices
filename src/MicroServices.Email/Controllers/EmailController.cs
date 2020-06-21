using MicroServices.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroServices.Email.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        [HttpPost(nameof(Send_QQ))]
        public string Send_QQ(Models.EmailInfo model)
        {
            return this.GetMessage("QQ", model);
        }

        [HttpPost(nameof(Send_Outlook))]
        public string Send_Outlook(Models.EmailInfo model)
        {
            return this.GetMessage("Outlook", model);
        }

        [HttpPost(nameof(Send_Google))]
        public string Send_Google(Models.EmailInfo model)
        {
            return this.GetMessage("Google", model);
        }

        private string GetMessage(string name, Models.EmailInfo model)
        {
            return $"[{IPUtils.GetLocalIp()}]"
                + $"通过【{name}】邮件接口向【{model.Email}】发送邮件，"
                + $"标题: {model.Title}，"
                + $"内容：{model.Body}";
        }
    }
}
