using Dapper;
using Randim.Common.DataAccess.Factory;
using Randim.Common.DataAccess.Interfaces;

namespace Randim.Common.DataAccess.Repository;

public abstract class BaseRepository(IDbConnectionFactory dbConnectionFactory) : IBaseRepository
{
    private readonly HashSet<string> _allowedTables = new() { "appusers", "posts", "comments" };
    private readonly HashSet<string> _allowedColumns = new() { "Id", "UserId", "Content" };

    public async Task<bool> ExistsAsync(
        string table,
        int id,
        CancellationToken cancellationToken = default
    )
    {
        if (!_allowedTables.Contains(table.ToLower()))
            throw new ArgumentException("Invalid table name.");
        var sql = $@"SELECT EXISTS(SELECT 1 FROM {table} WHERE {table}.id = @id)";
        using var connection = await dbConnectionFactory.CreateConnectionAsync();
        return await connection.ExecuteScalarAsync<bool>(
            new CommandDefinition(sql, id, cancellationToken: cancellationToken)
        );
    }

    public async Task<bool> ConnectedAsync(
        string table,
        int userId1,
        int userId2,
        CancellationToken cancellationToken = default
    )
    {
        if (!_allowedTables.Contains(table.ToLower()))
            throw new ArgumentException("Invalid table name.");
        var sql =
            $@"SELECT EXISTS(SELECT 1 FROM {table} WHERE UserId1 = @UserId1 AND UserId2 = @UserId2)";
        using var connection = await dbConnectionFactory.CreateConnectionAsync();
        return await connection.ExecuteScalarAsync<bool>(
            new CommandDefinition(
                sql,
                new { UserId1 = userId1, UserId2 = userId2 },
                cancellationToken: cancellationToken
            )
        );
    }
}
