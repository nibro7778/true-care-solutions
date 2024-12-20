using Staffs.Application;

namespace Staffs.Infrastructure;

public class StaffRepository(IDbConnectionFactory connections) : IStaffRepository
{
    public async Task Add(Staff staff)
    {
        const string sql = $"INSERT INTO {StaffTable} (Id, FirstName, LastName) VALUES (@Id, @FirstName, @LastName)";
        var connection = await connections.OpenAsync();
        await connection.ExecuteAsync(sql, staff, TransactionContext.GetCurrentTransaction());
    }

    public async Task<Staff?> Get(Guid id)
    {
        const string sql = $"SELECT * FROM {StaffTable} WHERE Id=@id";
        var command = new CommandDefinition(sql, new { id }, TransactionContext.GetCurrentTransaction());
        var connection = await connections.OpenAsync();
        return await connection.QuerySingleOrDefaultAsync<Staff>(command);
    }
}