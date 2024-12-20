using Clients.Application;
namespace Clients.Infrastructure;

public class ClientRepository(IDbConnectionFactory connections) : IClientRepository
{
    public async Task<bool> Exists(Guid id)
    {
        const string sql = $"SELECT count(1) FROM {ClientTable} WHERE Id = @id";
        var command = new CommandDefinition(sql, new { id });
        return await (await connections.OpenAsync()).ExecuteScalarAsync<int>(command) > 0;       
    }
    public async Task Add(Client client)
    {
        const string sql = $"INSERT INTO {ClientTable} (Id, FirstName, LastName) VALUES (@Id, @FirstName, @LastName)";
        var connection = await connections.OpenAsync();
        await connection.ExecuteAsync(sql, client, TransactionContext.GetCurrentTransaction());
    }
    public async Task<Client?> Get(Guid id)
    {
        const string sql = $"SELECT * FROM {ClientTable} WHERE Id=@id";
        var command = new CommandDefinition(sql, new { id }, TransactionContext.GetCurrentTransaction());
        var connection = await connections.OpenAsync();
        return await connection.QuerySingleOrDefaultAsync<Client>(command);
    }
}