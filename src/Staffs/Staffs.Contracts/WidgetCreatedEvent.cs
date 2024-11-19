using Common.Infrastructure.Integration;

namespace Staffs.Contracts;

public class WidgetCreatedEvent : IntegrationEvent
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public decimal Price { get; init; }
}