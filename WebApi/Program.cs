using System.Text.Json;
using System.Text.Json.Serialization;
using Domain;
using Domain.Interfaces;
using Infrastructure;
using Infrastructure.Repositories;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Services;
using WebApi.Midlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddJsonOptions(
            options => { 
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });;



builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<ITransacoesService, TransacoesService>();
var connectionString  = Environment.GetEnvironmentVariable("PostgreSQL");

builder.Services.AddDbContext<DatabaseContext>(options => {
    options.UseNpgsql(connectionString);
});
var dataContext = builder.Services.BuildServiceProvider().GetRequiredService<DatabaseContext>();
dataContext.Database.EnsureCreated();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlerMiddleware>();


app.Run();