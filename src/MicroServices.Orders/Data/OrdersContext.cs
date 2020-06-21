using MicroServices.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroServices.Orders.Data
{
    public class OrdersContext : DbContext
    {
        public OrdersContext(DbContextOptions<OrdersContext> options)
            : base(options)
        {
        }

        public DbSet<OrderInfo> OrderInfoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderInfo>(builder =>
            {
                builder.HasData(
                    new OrderInfo { Id = 1, Name = "Surface Pro 5", Price = 8888 },
                    new OrderInfo { Id = 2, Name = "Surface Pro 6", Price = 9999 },
                    new OrderInfo { Id = 3, Name = "Surface Pro 7", Price = 8899 });
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
