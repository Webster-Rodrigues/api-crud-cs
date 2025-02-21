using System.Text.Json.Serialization;
using APICatalogo.Context;
using APICatalogo.Extensions;
using APICatalogo.Filters;
using APICatalogo.Logging;
using APICatalogo.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Define que o tratamento da serialização vai ignorar quando ocorrer uma referência ciclica 
builder.Services.AddControllers(options =>
    {
        options.Filters.Add(typeof(ApiExceptionFilter));
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

string? mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

//Inclui o serviço do EntityFramework em um contêiner inativo.
//Cosigo injetar uma instância de appDbContext ao onde for necessário
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql
                    (mySqlConnection,ServerVersion.AutoDetect(mySqlConnection)));

//Registra o filtro
builder.Services.AddScoped<ApiLoggingFilter>();
builder.Services.AddScoped<IProductRepository, ProductRepository>(); //Registra o serviço do repository
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>(); //Toda vez que ICategoryRepository for referenciada
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));               //ele passa a implementação do CategoryRepository
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration()
{
    LogLevel = LogLevel.Information
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.ConfigureExceptionHandler();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();