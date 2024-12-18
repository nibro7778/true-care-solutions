using Microsoft.Data.SqlClient;
namespace Common.Infrastructure;

public interface IDbConnectionFactory
{
    Task<SqlConnection> OpenAsync();    
}