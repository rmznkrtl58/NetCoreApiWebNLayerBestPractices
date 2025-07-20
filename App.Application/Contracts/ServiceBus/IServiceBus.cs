using App.Domain.Events;

namespace App.Application.Contracts.ServiceBus
{
    public interface IServiceBus
    {
        Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : IEventOrMessage;
        Task SendAsync<T>(T message, string queueName, CancellationToken cancellationToken = default) where T : IEventOrMessage;
        //Bir developer herhangi bir entityi T yerine yollamaması için IMessage veya IEvent Türünde olmasını bekliyorum
    }
}
