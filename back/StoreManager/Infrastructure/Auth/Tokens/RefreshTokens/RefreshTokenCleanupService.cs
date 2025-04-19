using Microsoft.EntityFrameworkCore;
using StoreManager.Infrastructure.DB;

namespace StoreManager.Infrastructure.Auth.Tokens.RefreshTokens;

public class RefreshTokenCleanupService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly TimeSpan _cleanupInterval;
    public RefreshTokenCleanupService(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        _serviceProvider = serviceProvider;
        int intervalMinutes = configuration.GetValue<int>("TokenCleanupSettings:CleanupIntervalHours");
        _cleanupInterval = TimeSpan.FromHours(intervalMinutes);
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await DeleteExpiredRefreshTokens();
            await Task.Delay(_cleanupInterval, stoppingToken);
        }
    }
    private async Task DeleteExpiredRefreshTokens()
    {
        using (var scope = _serviceProvider.CreateScope())
        {

            var dbContext = scope.ServiceProvider.GetRequiredService<WarehouseDbContext>();
            await dbContext.Database.ExecuteSqlRawAsync("DELETE FROM \"RefreshTokens\" WHERE \"ExpiresOnUtc\" < NOW()");
            await dbContext.SaveChangesAsync();
        }
    }
}