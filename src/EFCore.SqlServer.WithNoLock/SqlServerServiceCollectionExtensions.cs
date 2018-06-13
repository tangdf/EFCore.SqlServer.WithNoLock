using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;
using Microsoft.EntityFrameworkCore.Query.Sql;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.WithNoLock;


// ReSharper disable once CheckNamespace
namespace Microsoft.EntityFrameworkCore
{
    public static class SqlServerDbContextOptionsExtensions
    {
        public static DbContextOptionsBuilder EnableSqlServerWithNoLock(this DbContextOptionsBuilder optionsBuilder)
        {
            var sqlServerOptionsExtension = optionsBuilder.Options.FindExtension<SqlServerOptionsExtension>();
            if (sqlServerOptionsExtension == null)
                return optionsBuilder;

            optionsBuilder = optionsBuilder
                .ReplaceService<IQuerySqlGeneratorFactory, WithNoLockSqlServerQuerySqlGeneratorFactory>();
            return optionsBuilder.ReplaceService<IMaterializerFactory, WithNoLockMaterializerFactory>();
        }
    }
}
