using System.Collections.Generic;
using System.Threading.Tasks;
using MicroServices.Models;
using WebApiClientCore;
using WebApiClientCore.Attributes;

namespace MicroServices.Client
{
    [LoggingFilter]
    [HttpHost("http://localhost:8080")]
    public interface IOrdersApi : IHttpApi
    {
        [HttpGet("/api/orders")]
        Task<List<OrderInfo>> Get();

        [HttpGet("/api/orders/{id}")]
        Task<OrderInfo> GetById(long id);
    }
}
