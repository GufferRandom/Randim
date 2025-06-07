using Dapper;
using Randim.Common.DataAccess.Factory;
using Randim.Common.DataAccess.Interfaces;

namespace Randim.Common.DataAccess.Repository;

public abstract class BaseRepository(IDbConnectionFactory dbConnectionFactory) : IBaseRepository
{
    private readonly HashSet<string> _allowedTables = new()
    {
        "appusers",
        "posts",
        "comments",
        "confirmed_friends",
    };
    private readonly HashSet<string> _allowedColumns = new()
    {
        "id",
        "user_id",
        "content",
        "user_id_1",
        "user_id_2",
        "confirmed",
    };
    private readonly HashSet<string> _allowedTypes = new() { "int32", "string", "bool", "guid" };

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

    public async Task<bool> ExistsAsync<T>(
        string table,
        string column,
        T value,
        CancellationToken cancellationToken = default
    )
    {
        if (!_allowedTables.Contains(table.ToLower()))
            throw new ArgumentException("Invalid table name.");
        if (!_allowedColumns.Contains(column.ToLower()))
            throw new ArgumentException("Invalid column name.");
        var type = typeof(T).Name.ToLower();
        if (!_allowedTypes.Contains(type))
            throw new ArgumentException("Invalid type.");
        var sql = $@"SELECT EXISTS(SELECT 1 FROM {table} WHERE {table}.{column} = @value)";
        using var connection = await dbConnectionFactory.CreateConnectionAsync();
        return await connection.ExecuteScalarAsync<bool>(
            new CommandDefinition(sql, new { value }, cancellationToken: cancellationToken)
        );
    }

    public Task<(int, int)> NormalizingUserIds(int userId1, int userId2)
    {
        if (userId1 == userId2)
            throw new ArgumentException("User Ids must be different.");
        var firstId = Math.Min(userId1, userId2);
        var secondId = Math.Max(userId1, userId2);
        return Task.FromResult((firstId, secondId));
    }
}
