namespace App.WebAPI.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerGenExt(this IServiceCollection services)
        {
            //swagger yapılandırması
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new() { Title = "App.WebAPI", Version = "v1" }); });
            return services;
        }
        public static IApplicationBuilder UseSwaggerExt(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
