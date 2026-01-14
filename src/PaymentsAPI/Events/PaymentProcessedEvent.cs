
namespace PaymentsAPI.Events {
    public record PaymentProcessedEvent(Guid OrderId, Guid UserId, Guid GameId, decimal Price, string Status);
}
