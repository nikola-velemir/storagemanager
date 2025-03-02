namespace StoreManager.Infrastructure.Auth
{
    public static class CorsPolicy
    {
        public static IServiceCollection AddCoursPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
             {
                 options.AddPolicy("AllowSpecificOrigin", builder =>
                 {
                     builder.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
                 });

             });
            return services;
        }
    }
}
