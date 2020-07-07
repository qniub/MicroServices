using System;
using System.Collections.Generic;
using Ocelot.Configuration.File;

namespace MicroServices.Api
{
    public class OcelotConfig
    {
        public static void Configuration(FileConfiguration config)
        {
            config.GlobalConfiguration.BaseUrl = "http://localhost:80";

            config.GlobalConfiguration.ServiceDiscoveryProvider.Host =
                Environment.GetEnvironmentVariable("CONSUL_HOST");
            config.GlobalConfiguration.ServiceDiscoveryProvider.Type = "Consul";

            config.Routes.Add(new FileRoute
            {
                DownstreamPathTemplate = "/",
                DownstreamScheme = "https",
                DownstreamHostAndPorts = new List<FileHostAndPort>
                {
                    new FileHostAndPort
                    {
                        Port = 443,
                        Host = "checkip.amazonaws.com",
                    },
                },

                UpstreamPathTemplate = "/api/values",
                UpstreamHttpMethod = new List<string> { "Get" },
            });

            config.Routes.Add(new FileRoute
            {
                ServiceName = "service_orders",
                DownstreamPathTemplate = "/api/orders",

                UpstreamPathTemplate = "/api/orders",
                UpstreamHttpMethod = new List<string> { "Get" },

                LoadBalancerOptions  = new FileLoadBalancerOptions
                {
                    Type = "LeastConnection",
                },
            });
        }
    }
}
