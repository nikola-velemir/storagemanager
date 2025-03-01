using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StoreManager.Auth;
using StoreManager.DTO;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<TokenGenerator>();

builder.Services.AddAuthorization();
builder.Services.AddJwtAuthentications();
var app = builder.Build();

app.MapGet("/test", (TokenGenerator tokenGenerator) => { return new { access_token = tokenGenerator.GenerateToken("kurac") }; });


app.UseRouting();
app.MapControllers();

app.Run();
