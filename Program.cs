using System.Reflection;
using AspNetCore.Identity.MongoDbCore.Models;
using E_Commerce.Api.DataLayer.Database;
using E_Commerce.Api.HostedServices;
using E_Commerce.Api.Models;
using E_Commerce.Api.Profiles;
using E_Commerce.Api.SecurityLayer.Authentication;
using E_Commerce.Api.SecurityLayer.Managers;
using E_Commerce.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<DatabaseConfiguration>(builder.Configuration.GetSection(nameof(DatabaseConfiguration)));
builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection(nameof(JwtConfiguration)));
builder.Services.AddSingleton<IDatabaseConfiguration>(sp =>
    sp.GetRequiredService<IOptions<DatabaseConfiguration>>().Value);
builder.Services.AddSingleton<IJwtConfiguration>(sp =>
    sp.GetRequiredService<IOptions<JwtConfiguration>>().Value);

var dbConfig = builder.Configuration.GetSection(nameof(DatabaseConfiguration)).Get<DatabaseConfiguration>();

builder.Services.AddSingleton<IMongoClient>(new MongoClient(connectionString: dbConfig.ConnectionString));
builder.Services.AddIdentity<MongoIdentityUser<string>, MongoIdentityRole<string>>()
    .AddMongoDbStores<MongoIdentityUser<string>, MongoIdentityRole<string>, string>(dbConfig.ConnectionString,
        dbConfig.DatabaseName);
builder.Services.AddHostedService<ConfigureMongoDb>();
builder.Services.AddSingleton<IDbContext, DbContext>();
builder.Services.AddTransient<IImageServices, ImageServices>();
builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(ImageFileProfile)));
builder.Services.AddScoped<IAuthentication, Authentication>();
builder.Services.AddScoped<IUserManager, UserManager>();

var jwtConfig = builder.Configuration.GetSection(nameof(JwtConfiguration)).Get<JwtConfiguration>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtConfig.ValidIssuer,
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();