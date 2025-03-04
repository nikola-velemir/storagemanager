using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.AppSetup;
using StoreManager.Infrastructure.Auth;
using StoreManager.Infrastructure.DB;

var builder = WebApplication.CreateBuilder(args);


builder.Services.InjectDependencies(builder.Configuration);
builder.Services.AddControllers();


builder.Services.AddJwtAuthentications();
builder.Services.AddAuthorization();


builder.Services.AddCoursPolicy();


var app = builder.Build();

app.UseCors("AllowSpecificOrigin");

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
