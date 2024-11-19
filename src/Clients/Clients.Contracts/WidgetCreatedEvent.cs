﻿using Common.Infrastructure.Integration;

namespace Clients.Contracts;

public class WidgetCreatedEvent : IntegrationEvent
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public decimal Price { get; init; }
}