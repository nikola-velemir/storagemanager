namespace StoreManager.Infrastructure.MiddleWare
{
    public static class KestrelConfig
    {
        public static IWebHostBuilder SetupKestrel(this IWebHostBuilder services)
        {
            services.ConfigureKestrel(serverOptions =>
            {
                serverOptions.ListenAnyIP(5205);
            });
            return services;
        }
    }
}
