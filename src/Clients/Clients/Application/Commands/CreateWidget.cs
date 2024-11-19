﻿using Common.Infrastructure.Integration;
using Microsoft.Extensions.Logging;
using Clients.Contracts;

namespace Clients.Application.Commands;

public static class CreateWidget
{
    public record Command(Guid Id, string Name, decimal Price) : IRequest;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(m => m.Id).NotEmpty();
            RuleFor(m => m.Name).NotEmpty().MaximumLength(255);
            RuleFor(m => m.Price).GreaterThan(0).LessThan(1000000);
        }
    }

    public class Handler(IWidgetRepository widgets, IntegrationEventRepository events, ILogger<Handler> log) : IRequestHandler<Command>
    {
        public async Task Handle(Command command, CancellationToken token)
        {
            var id = command.Id;
            var name = command.Name;
            var price = command.Price;

            if (await widgets.Exists(id))
            {
                BusinessRuleValidationException.ThrowAlreadyExists<Widget>(id);
            }

            if (await widgets.Exists(name))
            {
                BusinessRuleValidationException.ThrowAlreadyExists<Widget>(name);
            }

            log.LogInformation("Creating widget {id} {name} {price}", id, name, price);
            var widget = Widget.Create(id, name, price);
            await widgets.Add(widget);
            await events.SaveAsync(new WidgetCreatedEvent { Id = widget.Id, Name = widget.Name, Price = widget.Price }, token);
        }
    }
}