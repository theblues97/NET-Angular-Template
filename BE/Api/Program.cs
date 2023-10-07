using Microsoft.EntityFrameworkCore;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Core.Dependency;
using Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Custom add
builder.Services.AddDbContextPool<AppDbContext>((service, opt) =>
{
    string baseDir = AppDomain.CurrentDomain.BaseDirectory;

    if (baseDir.Contains("bin"))
    {
        int index = baseDir.IndexOf("bin");
        baseDir = baseDir.Substring(0, index);
        baseDir = Directory.GetParent(baseDir)?.Parent?.FullName ?? "";
    }

    var DbPath = $"Data Source={Path.Join(baseDir, "product.db")}";
    opt.UseSqlite(DbPath);
    //var tenant = service.GetRequiredService<ITenantResolver>().GetCurrentTenant();
    //opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    //opt.UseSqlite(tenant.ConnectionString);
});

//AutoFac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(b => b.AutofacRegister());
var app = builder.Build();


//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseSwagger();
//app.UseSwaggerUI();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();