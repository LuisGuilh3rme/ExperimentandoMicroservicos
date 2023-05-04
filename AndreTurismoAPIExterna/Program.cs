using AndreTurismoAPIExterna.Data;
using AndreTurismoAPIExterna.Services;
using RabbitMQ.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AndreTurismoAPIExternaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AndreTurismoAPIExternaContext") ?? throw new InvalidOperationException("Connection string 'AndreTurismoAPIExternaContext' not found.")));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<EnderecoAPIService>();
builder.Services.AddSingleton<ClienteAPIService>();
builder.Services.AddSingleton<HotelAPIService>();
builder.Services.AddSingleton<PassagemAPIService>();
builder.Services.AddSingleton<PacoteAPIService>();
builder.Services.AddSingleton<ConnectionFactory>();

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
