using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Consul;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Distributed;
using NXHub.Extensions.Caching;

namespace MicroServices.Web.Pages
{
    public class IndexModel : PageModel
    {
        private const string CONSUL_SERVICES = nameof(CONSUL_SERVICES);

        public IndexModel()
        {
            Services = new Dictionary<string, CatalogService[]>();
        }

        public bool IsHit { get; set; }
        public IDictionary<string, CatalogService[]> Services { get; set; }

        public async Task OnGetAsync(
            [FromServices] IConsulClient consul,
            [FromServices] IDistributedCache cache)
        {
            if (!cache.TryGetObject(CONSUL_SERVICES, out IDictionary<string, CatalogService[]> values))
            {
                QueryResult<Dictionary<string, string[]>> services = await consul.Catalog.Services();

                if (services.StatusCode == HttpStatusCode.OK)
                {
                    values = new Dictionary<string, CatalogService[]>();

                    foreach (string serviceName in services.Response.Keys)
                    {
                        if (serviceName != "consul")
                        {
                            QueryResult<CatalogService[]> service =
                                await consul.Catalog.Service(serviceName);

                            if (service.StatusCode == HttpStatusCode.OK)
                            {
                                values[serviceName] = service.Response;
                            }
                        }
                    }

                    cache.SetObject(CONSUL_SERVICES, values, TimeSpan.FromSeconds(10));
                }
            }
            else
            {
                this.IsHit = true;
            }

            Services = values ?? Services;
        }
    }
}
