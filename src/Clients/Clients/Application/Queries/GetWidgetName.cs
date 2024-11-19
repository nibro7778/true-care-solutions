﻿namespace Clients.Application.Queries;

public static class GetWidgetName
{
    public record Query(Guid Id) : IRequest<string>;

    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(m => m.Id).NotEmpty();
        }
    }

    public class Handler(IDbConnectionFactory connections) : IRequestHandler<Query, string>
    {
        public async Task<string> Handle(Query query, CancellationToken token)
        {
            var id = query.Id;

            const string sql = $"SELECT name FROM {WidgetsTable} WHERE {IdColumn}=@id";
            var command = new CommandDefinition(sql, new { id }, cancellationToken: token);
            var connection = await connections.OpenAsync();
            var result = await connection.ExecuteScalarAsync<string>(command);
            if (result == null)
            {
                throw new BusinessRuleValidationException($"Widget with id {id} not found");
            }

            return result;
        }
    }
}