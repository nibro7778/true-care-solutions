using Clients.Application;

namespace Clients.Infrastructure;

public class ClientRepository(IDbConnectionFactory connections) : IClientRepository
{
    public async Task<bool> Exists(Guid id)
    {
        const string sql = $"SELECT count(1) FROM {ClientTable} WHERE Id = @id";
        var command = new CommandDefinition(sql, new { id });
        return await (await connections.OpenAsync()).ExecuteScalarAsync<int>(command) > 0;
        return true;
    }

    public async Task Add(Client client)
    {
        const string sql = $"INSERT INTO {ClientTable} (Id, FirstName, LastName) VALUES (@Id, @Name, @Price)";
        var command = new CommandDefinition(sql, client);
        var connection = await connections.OpenAsync();
        await connection.ExecuteAsync(command);
    }
}