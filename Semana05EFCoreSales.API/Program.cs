using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Semana05EFCoreSales.DOMAIN.Core.Interfaces;
using Semana05EFCoreSales.DOMAIN.Infrastructure.Data;
using Semana05EFCoreSales.DOMAIN.Infrastructure.Mapping;
using Semana05EFCoreSales.DOMAIN.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Get ConnectionString
var connectionString = builder.Configuration.GetConnectionString("DevConnection");
//Add DbContext
builder
    .Services
    .AddDbContext<SalesUESANContext>
    (options => options.UseSqlServer(connectionString));

//Add services to the container
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();

//Add Automapper
var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutomapperProfile());
});
var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
