using App.WebAPI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace App.WebAPI.Extensions
{
    public static class ControllerExtensions
    {
        public static IServiceCollection AddControllersWithFiltersExt(this IServiceCollection services)
        {
            //Fluent Validation Filter Yapılandırması
            services.AddControllers(opt =>
            {

                opt.Filters.Add<FluentValidationFilter>();
                //referans tipler için null kontrolünü gerçekleştirmeyecek
                opt.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            });
            //NotFoundFilterYapılanması
            services.AddScoped(typeof(NotFoundFilter<,>));
            // Add services to the container.
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            //Apinin .net core tarafında default validasyon hata mesajlarını kapatman
            services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.SuppressModelStateInvalidFilter = true;
            });
            return services;
        }
    }
}