namespace Randim.Common.DataAccess.Interfaces;

public interface IBaseRepository
{
    Task<bool> ExistsAsync(string table, int id, CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync<T>(
        string table,
        string column,
        T value,
        CancellationToken cancellationToken = default
    );
    Task<(int, int)> NormalizingUserIds(int userId1, int userId2);
}
