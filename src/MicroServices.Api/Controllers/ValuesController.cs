using Microsoft.AspNetCore.Mvc;

namespace MicroServices.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public string[] Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
