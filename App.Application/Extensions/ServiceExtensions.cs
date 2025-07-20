using App.Application.Features.ServiceInterfaces;
using App.Application.Features.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace App.Application.Extensions
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

            //Fluent Validation Yapılandırması
            services.AddFluentValidationAutoValidation();
            //Service katmanım üzerinde validation yapcağımdan dolayı getexecutingAssmbly kullandım.
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
           
            return services;
        }
    }
}
