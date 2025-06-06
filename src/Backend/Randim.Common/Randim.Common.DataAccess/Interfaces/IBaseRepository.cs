namespace Randim.Common.DataAccess.Interfaces;

public interface IBaseRepository<T>
{
    Task<IEnumerable<T>> InnerJoinAsync(
        string table1,
        string table2,
        string column1,
        string column2,
        CancellationToken cancellationToken = default
    );
}
