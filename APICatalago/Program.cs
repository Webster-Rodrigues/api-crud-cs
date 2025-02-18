using APICatalago.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

string? mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

//Inclui o serviço do EntityFramework em um contêiner inativo.
//Cosigo injetar uma instância de appDbContext ao onde for necessário
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql
                    (mySqlConnection,ServerVersion.AutoDetect(mySqlConnection)));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();