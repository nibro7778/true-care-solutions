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

    public class Handler(IClientRepository clientRepository, ILogger<Handler> log) : IRequestHandler<Command>
    {
        public async Task Handle(Command command, CancellationToken token)
        {
            var client = new Client() { Id = command.Id, FirstName = command.FirstName, LastName = command.LastName };
            await clientRepository.Add(client);
        }
    }
}