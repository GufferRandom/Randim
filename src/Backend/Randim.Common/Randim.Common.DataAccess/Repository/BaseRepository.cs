using Dapper;
using Randim.Common.DataAccess.Factory;
using Randim.Common.DataAccess.Interfaces;

namespace Randim.Common.DataAccess.Repository;

public abstract class BaseRepository<T>(IDbConnectionFactory dbConnectionFactory)
    : IBaseRepository<T>
{
    private readonly HashSet<string> _allowedTables = new() { "Users", "Posts", "Comments" };
    private readonly HashSet<string> _allowedColumns = new() { "Id", "UserId", "Content" };

    public virtual async Task<IEnumerable<T>> InnerJoinAsync(
        string table1,
        string table2,
        string column1,
        string column2,
        CancellationToken cancellationToken = default
    )
    {
        ValidatingInput(table1, table2, column1, column2);
        using var connection = await dbConnectionFactory.CreateConnectionAsync();
        var sql =
            $@"
            SELECT *
            FROM {table1}
            INNER JOIN {table2}
            ON {table1}.{column1} = {table2}.{column2}";
        var result = await connection.QueryAsync<T>(
            new CommandDefinition(sql, cancellationToken: cancellationToken)
        );
        return result;
    }

    private void ValidatingInput(string table1, string table2, string column1, string column2)
    {
        if (!_allowedTables.Contains(table1) || !_allowedTables.Contains(table2))
            throw new ArgumentException("Invalid table name.");
        if (!_allowedColumns.Contains(column1) || !_allowedColumns.Contains(column2))
            throw new ArgumentException("Invalid column name.");
    }
}
