using StoreManager.Infrastructure.AppSetup;
using StoreManager.Infrastructure.Auth;
using StoreManager.Infrastructure.Auth.TokenGenerator;

var builder = WebApplication.CreateBuilder(args);

builder.Services.InjectDependencies();
builder.Services.AddControllers();

builder.Services.AddAuthorization();
builder.Services.AddJwtAuthentications();


builder.Services.AddCoursPolicy();


var app = builder.Build();

app.UseCors("AllowSpecificOrigin");


app.MapGet("/test", (ITokenGenerator tokenGenerator) => { return new { access_token = tokenGenerator.GenerateToken("kurac") }; });


app.UseRouting();
app.MapControllers();

app.Run();
