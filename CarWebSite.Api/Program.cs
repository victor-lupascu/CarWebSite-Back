using CarWebSite.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CarWebSite.BusinessLayer.Auth;
using CarWebSite.Api.Middleware;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// DbSession 
DbSession.ConnectionString = builder.Configuration
    .GetConnectionString("DefaultConnection")!;

// JWT Authentication configuration
var jwtKey = builder.Configuration["Jwt:Key"]
    ?? "presentation-dev-key-change-before-production-32chars";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "CarWebSiteApi";
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "CarWebSiteClients";

//JWTSession - direct Auth access
JwtSession.Issuer = jwtIssuer;
JwtSession.Audience = jwtAudience;
JwtSession.SecretKey = jwtKey;
JwtSession.AccessTokenMinutes = int.TryParse(
    builder.Configuration["Jwt:AccessTokenMinutes"], out var accessTokenMinutes)
        ? accessTokenMinutes
        : 60;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtIssuer,
            ValidateAudience = true,
            ValidAudience = jwtAudience,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(item => item.Value?.Errors.Count > 0)
            .ToDictionary(
                item => item.Key,
                item => item.Value!.Errors.Select(error => error.ErrorMessage).ToArray());

        return new BadRequestObjectResult(new
        {
            status = StatusCodes.Status400BadRequest,
            title = "Invalid request",
            message = "The request contains invalid or missing fields.",
            errors
        });
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();   

var app = builder.Build();

app.UseCors("AllowFrontend");
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseMiddleware<ApiExceptionMiddleware>();

// JWT middleware 
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
