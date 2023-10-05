using Core.Context;
using Dal.SqLite.EF;
using Microsoft.EntityFrameworkCore;

namespace Dal.SqLite.Context
{
    public partial class MyContext: BaseDbContext<MyContext>
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products{ get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<ShopProduct> ShopProducts { get; set; }
        public DbSet<Order> Orders { get; set; }

        public MyContext(DbContextOptions options) : base(options)
        {
        }

        //public MyContext()
        //{
        //    string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        //    if (baseDir.Contains("bin"))
        //    {
        //        int index = baseDir.IndexOf("bin");
        //        baseDir = baseDir.Substring(0, index);
        //        baseDir = Directory.GetParent(baseDir)?.Parent?.FullName ?? "";
        //    }

        //    DbPath = Path.Join(baseDir, "product.db");
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    }
}
