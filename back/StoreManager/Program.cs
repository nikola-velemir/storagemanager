using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.AppSetup;
using StoreManager.Infrastructure.Auth;
using StoreManager.Infrastructure.Auth.TokenGenerator;
using StoreManager.Infrastructure.DB;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");
builder.Services.AddDbContext<WarehouseDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.InjectDependencies();
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
