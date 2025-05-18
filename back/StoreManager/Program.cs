using StoreManager.Infrastructure.Auth;
using StoreManager.Infrastructure.MiddleWare;
using StoreManager.Infrastructure.Notifications;
using StoreManager.Presentation.MiddleWare;
using StoreManager.Presentation.Product.Batch;

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

app.MapHub<NotificationsHub>("/hubs/notifications");

app.UseRouting();
app.MapProductBatchApi();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
