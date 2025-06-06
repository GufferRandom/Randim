namespace Randim.Common.DataAccess.Interfaces;

public interface IBaseRepository
{
    Task<bool> ExistsAsync(string table, int id, CancellationToken cancellationToken = default);
    Task<bool> ConnectedAsync(
        string table,
        int userId1,
        int userId2,
        CancellationToken cancellationToken = default
    );
}
