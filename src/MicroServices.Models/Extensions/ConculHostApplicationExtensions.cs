using System;
using Consul;
using Microsoft.Extensions.Hosting;

namespace Microsoft.AspNetCore.Builder
{
    public static class ConculHostApplicationExtensions
    {
        public static IHostApplicationLifetime RegisterToConsul(this IHostApplicationLifetime host)
        {
            string serviceName = Environment.GetEnvironmentVariable("SERVICE_NAME");

            string serviceHost = Environment.GetEnvironmentVariable("SERVICE_HOST");

            string serviceId = $"{serviceName}_{Guid.NewGuid()}";

            string serviceAddress = $"http://{serviceHost}";

            host.ApplicationStarted.Register(() =>
            {
                using IConsulClient client = CreateClient();

                client.Agent.ServiceRegister(new AgentServiceRegistration
                {
                    ID = serviceId,
                    Name = serviceName,

                    Port = 80,
                    Address = serviceAddress,

                    Check = new AgentServiceCheck
                    {
                        HTTP = $"{serviceAddress}/health",
                        Timeout = TimeSpan.FromSeconds(5),
                        Interval = TimeSpan.FromSeconds(5),
                        DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                    },
                }).Wait();

            });

            host.ApplicationStopped.Register(() =>
            {
                using IConsulClient client = CreateClient();

                client.Agent.ServiceDeregister(serviceId).Wait();
            });

            return host;
        }

        private static IConsulClient CreateClient()
        {
            return new ConsulClient(config =>
            {
                string address = Environment.GetEnvironmentVariable("CONSUL_HOST");

                config.Address = new Uri($"http://{address}:8500");
            });
        }
    }
}
