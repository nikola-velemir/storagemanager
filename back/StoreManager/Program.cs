using StoreManager.Infrastructure.Auth;
using StoreManager.Infrastructure.MiddleWare;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.SetupKestrel();
builder.Services.InjectDependencies(builder.Configuration);
builder.Services.AddControllers();


builder.Services.AddJwtAuthentications(builder.Configuration);
builder.Services.AddAuthorization();


builder.Services.AddCoursPolicy();


var app = builder.Build();

app.UseMiddleware<ExceptionHandler>();
app.UseCors("AllowSpecificOrigin");

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
