using Core.Context;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{

    public interface IIdentityDbContext
    {
    }
    public partial class AppDbContext : DbContext, IIdentityDbContext
	{
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<ShopProduct> ShopProducts { get; set; }
        public DbSet<Order> Orders { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
