using System.Data;

namespace Randim.Common.DataAccess.Factory;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateConnectionAsync();
}
