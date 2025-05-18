namespace StoreManager.Presentation.MiddleWare
{
    public static class CorsPolicy
    {
        public static IServiceCollection AddCoursPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
             {
                 options.AddPolicy("AllowSpecificOrigin", builder =>
                 {
                     builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                 });

             });
            return services;
        }
    }
}
