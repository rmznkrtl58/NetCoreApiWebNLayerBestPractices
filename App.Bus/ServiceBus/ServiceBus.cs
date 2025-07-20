using App.Application.Contracts.ServiceBus;
using App.Domain.Events;
using MassTransit;

namespace App.Bus.ServiceBus
{
    public class ServiceBus(ISendEndpointProvider _sendProvider,IPublishEndpoint _publishEndpoint) : IServiceBus
    {
        public async Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : IEventOrMessage
        {
            await _publishEndpoint.Publish(message, cancellationToken);
        }

        public async Task SendAsync<T>(T message,string queueName, CancellationToken cancellationToken = default) where T : IEventOrMessage
        {
            var endPoint = await _sendProvider.GetSendEndpoint(new Uri($"queue:{queueName}"));
            await endPoint.Send(message, cancellationToken);
        }
    }
}
