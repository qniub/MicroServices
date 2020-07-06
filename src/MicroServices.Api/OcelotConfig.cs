using System.Collections.Generic;
using Ocelot.Configuration.File;

namespace MicroServices.Api
{
    public class OcelotConfig
    {
        public static void Configuration(FileConfiguration config)
        {
            config.Routes.Add(new FileRoute
            {
                DownstreamPathTemplate = "/",
                DownstreamScheme = "https",
                DownstreamHostAndPorts = new List<FileHostAndPort>
                {
                    new FileHostAndPort
                    {
                        Host = "checkip.amazonaws.com",
                        Port = 443,
                    },
                },

                UpstreamPathTemplate = "/api/values",
                UpstreamHttpMethod = new List<string>
                {
                    "Get",
                },
            });

            config.GlobalConfiguration.BaseUrl = "http://localhost:5000";
        }
    }
}
