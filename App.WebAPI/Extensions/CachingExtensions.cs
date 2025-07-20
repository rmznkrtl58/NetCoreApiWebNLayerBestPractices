using App.Application.Contracts.Caching;
using App.Caching.Caching;

namespace App.WebAPI.Extensions
{
    public static class CachingExtensions
    {
        public static IServiceCollection AddCachingExt(this IServiceCollection services)
        {
            //Cacheleme yapılandırma
            services.AddMemoryCache();
            //db'ye gitmediğimiz için addSingleton kullandım.
            services.AddSingleton<ICacheService, CacheService>();
            return services;
        }
    }
}
