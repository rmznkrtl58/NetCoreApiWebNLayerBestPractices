using App.Domain.Events;
using MassTransit;

namespace App.Bus.Consumers
{
    public class ProductAddedEventConsumer : IConsumer<ProductAddedEvent>
    {   
        //Burada sadece applicationda yazılan serviclerim kullanılabilir 
        //Consumersim dinleyicim rabbitMq vasıtasıyla gönderilen event veya mesajı dinleyici olarak alıp contenti console ekranına yazdırdım.
        public Task Consume(ConsumeContext<ProductAddedEvent> context)
        {
            //Kuyruğu dinleme işlemi
            Console.WriteLine($"Gelen Event=> Eklenen Ürünün İçerikleri: {context.Message.Id}-{context.Message.Name}-{context.Message.Price}");
            return Task.CompletedTask;
        }
    }
}
