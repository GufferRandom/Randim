using System.Data;
using Npgsql;
using Randim.Common.DataAccess.Factory;

namespace Randim.Common.DataAccess.Context;

public class DbConnectionFactory(string connectionString) : IDbConnectionFactory
{
    private readonly string? _connectionString = connectionString;

    public async Task<IDbConnection> CreateConnectionAsync()
    {
        var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        return connection;
    }
}
