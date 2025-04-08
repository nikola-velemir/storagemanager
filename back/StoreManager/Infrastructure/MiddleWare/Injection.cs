using MediatR;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using StoreManager.Infrastructure.Auth.Service;
using StoreManager.Infrastructure.Auth.Tokens.AcessToken.Generator;
using StoreManager.Infrastructure.Auth.Tokens.RedisCache;
using StoreManager.Infrastructure.Auth.Tokens.RefreshToken.Generator;
using StoreManager.Infrastructure.Auth.Tokens.RefreshToken.Repository;
using StoreManager.Infrastructure.DB;
using StoreManager.Infrastructure.Document.Repository;
using StoreManager.Infrastructure.Document.Service;
using StoreManager.Infrastructure.Document.Service.FileService;
using StoreManager.Infrastructure.Document.Service.Reader;
using StoreManager.Infrastructure.Document.SupaBase.Service;
using StoreManager.Infrastructure.Invoice.Repository;
using StoreManager.Infrastructure.Invoice.Service;
using StoreManager.Infrastructure.MechanicalComponent.Repository;
using StoreManager.Infrastructure.MechanicalComponent.Service;
using StoreManager.Infrastructure.Provider.Repository;
using StoreManager.Infrastructure.Provider.Service;
using StoreManager.Infrastructure.User.Repository;
using StoreManager.Infrastructure.User.Service;

namespace StoreManager.Infrastructure.AppSetup
{
    public static class Injection
    {
        public static IServiceCollection InjectDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var redisConnectionString = configuration.GetConnectionString("Redis")
                ?? throw new InvalidProgramException("Redis connection not found");

            var connectionString = configuration.GetConnectionString("PostgresConnection") ??
                throw new InvalidProgramException("Postres connection not found");


            services.AddDbContext<WarehouseDbContext>(options => options.UseNpgsql(connectionString));
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString));

            services.AddHostedService<RefreshTokenCleanupService>();

            services.AddScoped<ICloudStorageService, SupabaseService>();

            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IDocumentService, DocumentService>();

            services.AddScoped<IInvoiceRepository, InvoiceRepository>();

            services.AddSingleton<IRedisCacheService, RedisCacheService>();

            services.AddSingleton<IAccessTokenGenerator, AccessTokenGenerator>();

            services.AddSingleton<IRefreshTokenGenerator, RefreshTokenGenerator>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRespository>();

            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IMechanicalComponentRepository, MechanicalComponentRepository>();
            services.AddScoped<IMechanicalComponentService, MechanicalComponentService>();

            services.AddScoped<IInvoiceItemRepository, InvoiceItemRepository>();
            services.AddScoped<IInvoiceService, InvoiceService>();

            services.AddScoped<IDocumentReaderFactory, DocumentReaderFactory>();
            services.AddScoped<PDFService>();
            services.AddScoped<ExcelService>();

            services.AddScoped<IProviderRepository, ProviderRepository>();
            services.AddScoped<IProviderService, ProviderService>();
            services.AddScoped<IProviderService, ProviderService>();

            services.AddScoped<IFileService, FileService>();

            services.AddMediatR(typeof(Program));

            return services;
        }
    }
}
