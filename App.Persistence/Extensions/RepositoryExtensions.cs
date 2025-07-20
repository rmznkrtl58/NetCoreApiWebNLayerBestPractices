using App.Application.Contracts.Interfaces;
using App.Domain.Options;
using App.Persistence.Context;
using App.Persistence.Interceptors;
using App.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Persistence.Extensions
{
    public static class RepositoryExtensions
    {   
        public static IServiceCollection AddRepositories(this IServiceCollection services,IConfiguration configuration)
        {
            //DbContextYapılanması
            services.AddDbContext<AppDbContext>(opt =>
            {   
                //Api->AppSettingJson->Development.json->ConnectionStringOption içerisindeki sabit değişkenimdeki ConnectionStringe göre al ve ConnectionStringOption içerisindeki SqlServer propuma ata burdaki işlemleri connectionStringe ata
                var connectionString = configuration.GetSection(ConnectionStringOption.key).Get<ConnectionStringOption>();
                //connectionString içerisinde data var merak etme demek "!"
                opt.UseSqlServer(connectionString!.SqlServer, SqlServerOptionsAction =>
                {
                    //migrationumun oluşacağı ve migration işlemlerimin yapılacağı yer RepositoryAssembly struchmın bulunduğu yere yapılacak olmasını programa söylüyorum
                    SqlServerOptionsAction.MigrationsAssembly(typeof(PersistenceAssembly).Assembly.FullName);
                });
                //Save changes tracker ile ilgili yaptığım yapılandırma
                opt.AddInterceptors(new AuditDbContextInterceptor());
            });
            //newlemelerden kurtarmak için kullanıyorum
            services.AddScoped(typeof(IGenericRepository<,>), typeof
                (GenericRepository<,>));
            services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));
            services.AddScoped(typeof(ICategoryRepository), typeof(CategoryRepository));
            services.AddScoped(typeof(IUnitOfWork),typeof(UnitOfWork));
            services.AddScoped<AppDbContext>();
            return services;
        }
    }
}
