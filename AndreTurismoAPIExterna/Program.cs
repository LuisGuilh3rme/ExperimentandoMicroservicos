using AndreTurismoAPIExterna.Services;

var builder = WebApplication.CreateBuilder(args);

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
