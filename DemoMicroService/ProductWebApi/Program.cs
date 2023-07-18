using Microsoft.EntityFrameworkCore;
using ProductWebAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/* Database Context Dependency Injection */
//var dbHost = "RIFAT\\SQLEXPRESS01";
//var dbName = "dms_product";
//var dbPassword = "5969";
//var dbHost = "RIFAT\\SQLEXPRESS01";
//var dbName = Environment.GetEnvironmentVariable("DB_NAME");
//var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbPassword = Environment.GetEnvironmentVariable("DB_ROOT_PASSWORD");
var connectionString = $"Data Source={dbHost};Initial Catalog={dbName};Encrypt=False;User ID=sa;Password={dbPassword}";
//var connectionString = $"Data Source={dbHost};Initial Catalog={dbName}";
builder.Services.AddDbContext<ProductDbContext>(opt => opt.UseSqlServer(connectionString));
/* ===================================== */



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
