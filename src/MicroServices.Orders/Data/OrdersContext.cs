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

        public DbSet<Order> Order { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(builder =>
            {
                builder.HasData(
                    new Order { Id = 1, Name = "Surface Pro 5", Price = 8888 },
                    new Order { Id = 2, Name = "Surface Pro 6", Price = 9999 },
                    new Order { Id = 3, Name = "Surface Pro 7", Price = 8899 });
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
