namespace Clients.Application.Queries;

public static class GetClient
{
    public record Query(Guid Id) : IRequest<Response>;

    public class Response
    {
        public Guid Id { get; init; }
        public required string FirstName { get; init; }
        public string? LastName { get; init; }
    }

    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(m => m.Id).NotEmpty();
        }
    }

    public class Handler(IClientRepository repository, IDbConnectionFactory connections) : IRequestHandler<Query, Response>
    {
        public async Task<Response> Handle(Query query, CancellationToken token)
        {
            var result = await repository.Get(query.Id);
            if (result == null)
            {
                throw new BusinessRuleValidationException($"Client with id {query.Id} not found");
            }
            return new Response { Id = result.Id, FirstName = result.FirstName, LastName = result.LastName };
        }
    }
}