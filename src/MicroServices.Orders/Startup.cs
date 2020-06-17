using System;
using MicroServices.Orders.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MicroServices.Orders
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connStr = Environment
                .GetEnvironmentVariable("PGSQL_CONNSTR")
                ?.Trim('"');

            services.AddDbContext<OrdersContext>(
                options => options.UseNpgsql(connStr));

            services.AddHealthChecks()
                .AddNpgSql(connStr);

            services.AddControllers();
        }

        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            IHostApplicationLifetime host)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");

                endpoints.MapControllers();
            });

            host.RegisterToConsul();

            host.TryMigrateDb<OrdersContext>(app);
        }
    }
}
