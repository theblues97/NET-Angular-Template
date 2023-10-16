using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
//using Microsoft.EntityFrameworkCore

namespace Infrastructure
{
	//public class AppDbContextFactory : IDbContextFactory<AppDbContext>
	//{
	//	private readonly IConfiguration _configuration;
	//	private readonly ICurrentTenant _currentTenant;

	//	public AppDbContextFactory() { }
	//	//public AppDbContextFactory(
	//	//	IConfiguration configuration,
	//	//	ICurrentTenant currentTenant)
	//	//{
	//	//	_configuration = configuration;
	//	//	_currentTenant = currentTenant;
	//	//}
	//	public AppDbContext CreateDbContext()
	//	{
	//		string baseDir = AppDomain.CurrentDomain.BaseDirectory;

	//		if (baseDir.Contains("bin"))
	//		{
	//			int index = baseDir.IndexOf("bin");
	//			baseDir = baseDir.Substring(0, index);
	//			baseDir = Directory.GetParent(baseDir)?.Parent?.FullName ?? "";
	//		}

	//		var DbPath = $"Data Source={Path.Join(baseDir, "product.db")}";

	//		var builder = new DbContextOptionsBuilder<AppDbContext>()
	//		.UseSqlite(DbPath);
	//		return new AppDbContext(builder.Options);
	//	}
	//}

	public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
	{

		public AppDbContext CreateDbContext(string[] args)
		{
			//string baseDir = AppDomain.CurrentDomain.BaseDirectory;

			//if (baseDir.Contains("bin"))
			//{
			//	int index = baseDir.IndexOf("bin");
			//	baseDir = baseDir.Substring(0, index);
			//	baseDir = Directory.GetParent(baseDir)?.Parent?.FullName ?? "";
			//}

			//var DbPath = $"Data Source={Path.Join(baseDir, "product.db")}";
			//var builder = new DbContextOptionsBuilder<AppDbContext>()
			//.UseSqlite(DbPath);

			// Get environment
			string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

			// Build config
			IConfiguration config = new ConfigurationBuilder()
				.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Api"))
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{environment}.json", optional: true)
				.AddEnvironmentVariables()
				.Build();

			// Get connection string
			var builder = new DbContextOptionsBuilder<AppDbContext>();
			var connectionString = config.GetConnectionString("Default");
			builder.UseSqlServer(connectionString);
			return new AppDbContext(builder.Options);
		}
	}
}
