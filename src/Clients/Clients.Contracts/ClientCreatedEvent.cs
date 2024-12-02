using Common.Infrastructure.Integration;

namespace Clients.Contracts;

public class ClientCreatedEvent : IntegrationEvent
{
    public Guid Id { get; init; }
    public required string FirstName { get; init; }
    public string LastName { get; init; } = null!;
}