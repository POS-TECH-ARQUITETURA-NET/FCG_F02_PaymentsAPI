
using MassTransit;
using PaymentsAPI.Events;

namespace PaymentsAPI.Consumers {
    public class OrdersConsumer : IConsumer<OrderPlacedEvent> {
        public async Task Consume(ConsumeContext<OrderPlacedEvent> context) {
            var msg = context.Message;
            // Regra simples de aprovação: aprova se preço <= 200, caso contrário rejeita
            var status = msg.Price <= 200m ? "Approved" : "Rejected";
            await context.Publish(new PaymentProcessedEvent(msg.OrderId, msg.UserId, msg.GameId, msg.Price, status));
            Console.WriteLine($"[Payments] Order {msg.OrderId} => {status}");
        }
    }
}
