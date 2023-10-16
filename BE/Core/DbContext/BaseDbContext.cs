//using Core.Dependency;
//using Core.MultiTenancy;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Infrastructure;
//using Microsoft.EntityFrameworkCore.Internal;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;

//namespace Core.Context
//{
//	public abstract class BaseDbContext : DbContext, ITransientDependency
//	{
//		[PropertyDependence]
//		public IServiceProvider ServiceProvider { get; set; } = null!;
//		ICurrentTenant CurrentTenant => ServiceProvider.GetRequiredService<ICurrentTenant>();
//		IConfiguration Configuration => ServiceProvider.GetRequiredService<IConfiguration>();
//		public BaseDbContext(DbContextOptions options) : base(options)
//		{
//		}

//		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//		{
//			var tenant = CurrentTenant.Tenant;
//			var connection = Configuration
//				.GetSection("ConnectionStrings")
//				.GetSection("Tenants")
//				.GetChildren()
//				.Where(x => x["TenantId"] == tenant)
//				.FirstOrDefault()?
//				.GetConnectionString("MySQLConnection");

//			optionsBuilder.UseSqlServer(connection, opt => opt.CommandTimeout(100));
//			//base.OnConfiguring(optionsBuilder);

//		}

//		public void Initial()
//		{
//			throw new NotImplementedException();
//		}
//	}
//}
