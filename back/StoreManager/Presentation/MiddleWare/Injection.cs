using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PdfSharp.Fonts;
using StoreManager.Application.GeoCoding;
using StoreManager.Domain;
using StoreManager.Fonts;
using StoreManager.Infrastructure;
using StoreManager.Infrastructure.Context;
using StoreManager.Infrastructure.GeoCoding;
using StoreManager.Infrastructure.MiddleWare.Injectors;

namespace StoreManager.Presentation.MiddleWare
{
    public static class Injection
    {
        public static IServiceCollection InjectDependencies(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgresConnection") ??
                                   throw new InvalidProgramException("Postgres connection not found");

            services.AddDbContext<WarehouseDbContext>(options => options.UseNpgsql(connectionString));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPasswordHasher<Domain.User.Model.User>, PasswordHasher<Domain.User.Model.User>>();
            services.InjectAuthDependencies(configuration);
            services.InjectDocumentDependencies(configuration);
            services.InjectUserDependencies(configuration);
            services.InjectMechanicalComponentDependencies(configuration);
            services.InjectInvoiceDependencies(configuration);
            services.InjectProviderDependencies(configuration);
            services.InjectProductDependencies(configuration);
            services.InjectExporterDependencies(configuration);
            services.InjectExportDependencies(configuration);

            services.AddScoped<IGeoCodingService, LocationIqService>();
            services.AddMediatR(typeof(Program));


            GlobalFontSettings.FontResolver = new FontResolver();

            return services;
        }
    }
}