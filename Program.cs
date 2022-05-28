using E_Commerce.Api.DataLayer.Database;
using E_Commerce.Api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<DatabaseConfiguration>(builder.Configuration.GetSection(nameof(DatabaseConfiguration)));
builder.Services.AddSingleton<IDatabaseConfiguration>(sp =>
    sp.GetRequiredService<IOptions<DatabaseConfiguration>>().Value);

var dbConfig = builder.Configuration.GetSection(nameof(DatabaseConfiguration)).Get<DatabaseConfiguration>();

builder.Services.AddSingleton<IMongoClient>(new MongoClient(connectionString: dbConfig.ConnectionString));
builder.Services.AddSingleton<IDbContext, DbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();