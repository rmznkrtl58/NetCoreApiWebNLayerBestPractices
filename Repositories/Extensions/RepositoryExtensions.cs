using App.Repositories.Context;
using App.Repositories.Interceptors;
using App.Repositories.Options;
using App.Repositories.Repositories;
using App.Repositories.RepositoryInterfaces;
using App.Repositories.UnitOfWorkPattern;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Repositories.Extensions
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
                    SqlServerOptionsAction.MigrationsAssembly(typeof(RepositoryAssembly).Assembly.FullName);
                });
                //Save changes tracker ile ilgili yaptığım yapılandırma
                opt.AddInterceptors(new AuditDbContextInterceptor());
            });
            //newlemelerden kurtarmak için kullanıyorum
            services.AddScoped(typeof(IGenericRepository<,>), typeof
                (GenericRepository<,>));
            services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));
            services.AddScoped(typeof(ICategoryRepository), typeof(CategoryRepository));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<AppDbContext>();
            return services;
        }
    }
}
