using Microsoft.Extensions.Logging;
using Clients.Contracts;

namespace Clients.Infrastructure.Handlers;

public class WidgetCreatedHandler(ILogger<WidgetCreatedEvent> log) : INotificationHandler<WidgetCreatedEvent>
{
    public Task Handle(WidgetCreatedEvent notification, CancellationToken cancellationToken)
    {
        log.LogInformation("Handling Widget created: {WidgetId}", notification.Name);
        return Task.CompletedTask;
    }
}