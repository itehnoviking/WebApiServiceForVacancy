using Microsoft.EntityFrameworkCore;
using System.Reflection;
using MediatR;
using Serilog;
using Serilog.Events;
using WebApiServiceForVacancy.Core.Interfaces.Services;
using WebApiServiceForVacancy.Data;
using WebApiServiceForVacancy.Domain.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Host.UseSerilog((ctx, lc) =>
    lc.WriteTo.File(builder.Configuration["Serilog:LogFilePath"],
            LogEventLevel.Information)
        .WriteTo.Console(LogEventLevel.Verbose));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<WebApiServiceForVacancyContext>(opt
    => opt.UseNpgsql(connectionString));

//DependencyInjection
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();

//AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

Assembly.Load("WebApiServiceForVacancy.CQRS");
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
