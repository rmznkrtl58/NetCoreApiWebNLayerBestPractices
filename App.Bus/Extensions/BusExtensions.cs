using App.Application.Contracts.ServiceBus;
using App.Bus.Consumers;
using App.Domain.Consts;
using App.Domain.Options;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace App.Bus.Extensions
{
    public static class BusExtensions
    {
        public static IServiceCollection AddBusExt(this IServiceCollection services, IConfiguration configuration)
        {
            //Api->appsettingjsonDevelopment->ServiceBusOption url'ime tip güvenliği sağlayarak class üzerinden urle ulaşabiliyorum.
            var serviceBusOption = configuration.GetSection(nameof(ServiceBusOption)).Get<ServiceBusOption>();

            //Scope olma sebebi product servicede kullandığım için kullanmasaydım Singelton olurdu
            services.AddScoped<IServiceBus, ServiceBus.ServiceBus>();

            //Masstransit RabbitMq yapılandırması
            services.AddMassTransit(x =>
            {
                x.AddConsumer<ProductAddedEventConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(serviceBusOption!.Url),
                        y => { });

                    //Uygulama ayağa kalktığı zaman sabit değişkenimdeki kuyruk ismiyle bir kuyruk oluşturacak bu kuyruğuda exchange maplicak
                    cfg.ReceiveEndpoint(ServiceBusConst.ProductAddedEventQueueName,
                        z =>
                        {
                            z.ConfigureConsumer<ProductAddedEventConsumer>(context);
                        });
                });
            });
          
            return services;
        }
    }
}
