﻿using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace Common.Infrastructure;

public class DbConnectionFactory(string connectionString, ILogger<DbConnectionFactory> log) : IDbConnectionFactory, IDisposable, IAsyncDisposable
{
    private SqlConnection? _connection;

    public async Task<SqlConnection> OpenAsync()
    {
        if (_connection is not null)
        {
            log.LogInformation("Reusing open connection");
            return _connection;
        }
        
        log.LogInformation("Opening new connection");
        _connection = new SqlConnection(connectionString);
        await _connection.OpenAsync();
        return _connection;
    }

    public void Dispose()
    {
        _connection?.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        if (_connection != null) await _connection.DisposeAsync();
    }
}