using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.EntityFrameworkCore.Query.Sql;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Sql.Internal;
using Microsoft.EntityFrameworkCore.Storage;

namespace Microsoft.EntityFrameworkCore.SqlServer.WithNoLock
{
    internal class WithNoLockSqlServerQuerySqlGenerator : SqlServerQuerySqlGenerator
    {
        public WithNoLockSqlServerQuerySqlGenerator(QuerySqlGeneratorDependencies dependencies,
            SelectExpression selectExpression, bool rowNumberPagingEnabled) : base(dependencies, selectExpression,
            rowNumberPagingEnabled)
        {

        }


        public override Expression VisitTable(TableExpression tableExpression)
        {
            var expression = base.VisitTable(tableExpression);

            if (tableExpression is WithNoLockTableExpression)
                Sql.Append(" With(nolock) ");
            return expression;
        }
    }
}
