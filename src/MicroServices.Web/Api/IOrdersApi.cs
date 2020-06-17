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
        Task<List<Order>> Get();

        [HttpGet("/api/orders/{id}")]
        Task<Order> GetById(long id);

        [HttpPost("/api/orders")]
        Task<Order> Create([JsonContent] Order order);

        [HttpPut("/api/orders/{id}")]
        Task Edit(long id, [JsonContent] Order order);

        [HttpDelete("/api/orders/{id}")]
        Task Delete(long id);
    }
}
