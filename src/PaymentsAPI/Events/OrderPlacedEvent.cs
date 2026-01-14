
namespace PaymentsAPI.Events {
    public record OrderPlacedEvent(Guid UserId, Guid GameId, decimal Price, Guid OrderId);
}
