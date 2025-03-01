using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StoreManager.Auth;
using StoreManager.DTO;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<TokenGenerator>();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x =>
    {
        var secret = Environment.GetEnvironmentVariable("JWT_SECRET", EnvironmentVariableTarget.User);

        if (string.IsNullOrEmpty(secret))
        {
            throw new InvalidOperationException("JWT Secret has not been set!");
        }

        var key = Encoding.UTF8.GetBytes(secret);

        x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidIssuer = "vnikola",
            ValidateLifetime = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true
        };
    });

var app = builder.Build();

app.MapGet("/test", (TokenGenerator tokenGenerator) => { return new { access_token = tokenGenerator.GenerateToken("kurac") }; });


app.UseRouting();
app.MapControllers();

app.Run();
