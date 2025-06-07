using Microsoft.Extensions.DependencyInjection;
using Randim.Common.DataAccess.Context;
using Randim.Common.DataAccess.Factory;

namespace Randim.Common.DataAccess.Extensions;

public static class AddDatabaseExtension
{
    public static IServiceCollection AddDatabase(
        this IServiceCollection services,
        string connectionString
    )
    {
        services.AddSingleton<IDbConnectionFactory>(_ => new DbConnectionFactory(connectionString));
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        return services;
    }
}
