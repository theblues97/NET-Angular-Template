using Microsoft.EntityFrameworkCore;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Core.Dependency;
using Infrastructure.Context;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Core.MultiTenancy;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Custom add
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContextPool<AppDbContext>((service, opt) =>
{
    //TODO: add db provider to optinal
	var tenant = service.GetRequiredService<ITenantResolver>().GetCurrentTenant();

	opt.UseSqlServer(tenant.ConnectionString, option => option.CommandTimeout(100));
});

//builder.Services.AddScoped<AppDbContextFactory>();
//builder.Services.AddScoped(sp => sp.GetRequiredService<AppDbContextFactory>().CreateDbContext());

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