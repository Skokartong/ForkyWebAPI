using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ForkyWebAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;
using ForkyWebAPI.Services.IServices;
using ForkyWebAPI.Services;
using ForkyWebAPI.Data.Repos.IRepos;
using ForkyWebAPI.Data.Repos;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

builder.Services.AddDbContext<ForkyContext>(options =>
    options.UseSqlServer(Environment.GetEnvironmentVariable("DefaultConnection")));

builder.Configuration.AddEnvironmentVariables();

var key = builder.Configuration["JwtKey"];
var issuer = builder.Configuration["JwtIssuer"];
var audience = builder.Configuration["JwtAudience"];

if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience))
{
    throw new Exception("JWT configuration values are missing.");
}

var keyBytes = Encoding.UTF8.GetBytes(key);
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();
builder.Services.AddRazorPages();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
            RoleClaimType = "role",
            NameClaimType = "name"
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("LocalReact", policy =>
    {
        policy.WithOrigins("http://localhost:5173/")
        .AllowAnyMethod()
        .AllowAnyHeader();
    });

    options.AddPolicy("AllowClientMvc", policy =>
    {
        policy.WithOrigins("http://localhost:7088/")
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

builder.Services.AddAuthorization();

builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IBookingRepo, BookingRepo>();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<IRestaurantRepo, RestaurantRepo>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAccountRepo, AccountRepo>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("LocalReact");
app.UseCors("AllowClientMvc");
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseHttpMethodOverride();

app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();
app.Run();



