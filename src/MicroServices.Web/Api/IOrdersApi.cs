using System.Collections.Generic;
using System.Threading.Tasks;
using MicroServices.Models;
using WebApiClientCore;
using WebApiClientCore.Attributes;

namespace MicroServices.Web.Api
{
    [LoggingFilter]
    public interface IOrdersApi : IHttpApi
    {
        [HttpGet("/api/orders")]
        Task<List<OrderInfo>> Get();

        [HttpGet("/api/orders/{id}")]
        Task<OrderInfo> GetById(long id);

        [HttpPost("/api/orders")]
        Task<OrderInfo> Create([JsonContent] OrderInfo order);

        [HttpPut("/api/orders/{id}")]
        Task Edit(long id, [JsonContent] OrderInfo order);

        [HttpDelete("/api/orders/{id}")]
        Task Delete(long id);
    }
}
