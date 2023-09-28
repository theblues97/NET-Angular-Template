using Microsoft.EntityFrameworkCore;
using Core.Repositories;
using Api.DI;
using Autofac;
using Autofac.Extensions.DependencyInjection;
//using Dal.InMemory.Context;
using Dal.SqLite.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Custom add
// string mySqlConnection = builder.Configuration.GetConnectionString("MySQLConnection") ?? "";
// builder.Services.AddDbContextPool<InMemoryContext>(opt =>
// {
//     opt.UseMySQL(mySqlConnection, mySqlOpt => { mySqlOpt.CommandTimeout(100); });
//     opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
// });

//builder.Services.AddDbContext<InMemoryContext>(c => c.UseInMemoryDatabase("Test"));
builder.Services.AddDbContext<SqLiteContext>();

//AutoFac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(b => b.AutofacRegister());

//builder.Services.AddTransient(typeof(ICmdRepository<>), typeof(Repository<>));
//builder.Services.AddTransient(typeof(IQueryRepository<>), typeof(Repository<>));
//builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();