using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Randim.Common.DataAccess.Factory;

namespace Randim.Common.DataAccess.Context;

public class DbConnectionFactory(IConfiguration configuration) : IDbConnectionFactory
{
    private readonly string? _connectionString = configuration.GetConnectionString("PostgresSQL");
    public IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);
}
