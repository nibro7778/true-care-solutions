using Common.Infrastructure.Integration;
using Microsoft.Extensions.Logging;
using Clients.Contracts;
using System.Xml.Linq;

namespace Clients.Application.Commands;

public static class CreateClient
{
    public record Command(Guid Id, string FirstName, string LastName) : IRequest;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(m => m.Id).NotEmpty();
            RuleFor(m => m.FirstName).NotEmpty().MaximumLength(255);
        }
    }

    public class Handler(IClientRepository clientRepository, IntegrationEventRepository events, ILogger<Handler> log) : IRequestHandler<Command>
    {
        public async Task Handle(Command command, CancellationToken token)
        {
            var id = command.Id;
            var firstName = command.FirstName;
            var lastName = command.LastName;
            
            log.LogInformation("Creating client {id} {firstName} {lastName}", id, firstName, lastName);
            var client = new Client() { Id = Guid.NewGuid(), FirstName = firstName, LastName = lastName };
            await clientRepository.Add(client);
            await events.SaveAsync(new ClientCreatedEvent { Id = client.Id, FirstName = client.FirstName, LastName = client.LastName }, token);
        }
    }
}