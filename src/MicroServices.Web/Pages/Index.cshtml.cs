using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Consul;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MicroServices.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConsulClient _consul;

        public IndexModel()
        {
            _consul = new ConsulClient(options =>
            {
                string address = Environment.GetEnvironmentVariable("CONSUL_HOST");

                options.Address = new Uri($"http://{address}:8500");
            });

            Services = new Dictionary<string, CatalogService[]>();
        }

        public IDictionary<string, CatalogService[]> Services { get; }

        public async Task OnGetAsync()
        {
            QueryResult<Dictionary<string, string[]>> services = await _consul.Catalog.Services();

            if (services.StatusCode == HttpStatusCode.OK)
            {
                foreach (string serviceName in services.Response.Keys)
                {
                    if (serviceName != "consul")
                    {
                        QueryResult<CatalogService[]> service = await _consul.Catalog.Service(serviceName);

                        if (service.StatusCode == HttpStatusCode.OK)
                        {
                            Services[serviceName] = service.Response;
                        }
                    }
                }
            }
        }
    }
}
