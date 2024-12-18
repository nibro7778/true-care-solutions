namespace Clients.Application.Queries;

public static class GetWidget
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

    public class Handler(IDbConnectionFactory connections) : IRequestHandler<Query, Response>
    {
        public async Task<Response> Handle(Query query, CancellationToken token)
        {
            var id = query.Id;

            //const string sql = $"SELECT * FROM {ClientTable} WHERE {IdColumn}=@id";
            const string sql = $"SELECT * FROM {ClientTable}";
            var command = new CommandDefinition(sql, new { id }, cancellationToken: token);
            var connection = await connections.OpenAsync();
            var result = await connection.QuerySingleOrDefaultAsync<Response>(command);
            if (result == null)
            {
                throw new BusinessRuleValidationException($"Client with id {id} not found");
            }

            return result;
        }
    }
}