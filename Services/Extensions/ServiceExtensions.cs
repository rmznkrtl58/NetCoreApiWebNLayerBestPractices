using App.Services.ExceptionHandlers;
using App.Services.Filters;
using App.Services.ServiceInterfaces;
using App.Services.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace App.Services.Extensions
{
    public static class ServiceExtensions
    {
       public static IServiceCollection AddServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped(typeof(IProductService), typeof
                (ProductService));
            services.AddScoped(typeof(IGenericService<>), typeof
                (GenericService<>));
            services.AddScoped(typeof(ICategoryService), typeof(CategoryService));
            //NotFoundFilterYapılanması
            services.AddScoped(typeof(NotFoundFilter<,>));
            //Fluent Validation Yapılandırması
            services.AddFluentValidationAutoValidation();
            //Service katmanım üzerinde validation yapcağımdan dolayı getexecutingAssmbly kullandım.
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //exception handlerlarımı yapılandırıyorum sıra önemli
            services.AddExceptionHandler<CriticalExceptionHandler>();
            services.AddExceptionHandler<GlobalExceptionHandler>();
            return services;
        }
    }
}
