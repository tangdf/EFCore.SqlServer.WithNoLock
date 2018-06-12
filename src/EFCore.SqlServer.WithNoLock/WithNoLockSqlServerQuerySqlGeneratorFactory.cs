using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Query.Sql;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Sql.Internal;

namespace Microsoft.EntityFrameworkCore.SqlServer.WithNoLock
{
    internal sealed class WithNoLockSqlServerQuerySqlGeneratorFactory :SqlServerQuerySqlGeneratorFactory
    {
        private readonly ISqlServerOptions _sqlServerOptions;
        public WithNoLockSqlServerQuerySqlGeneratorFactory(QuerySqlGeneratorDependencies dependencies, ISqlServerOptions sqlServerOptions, INodeTypeProviderFactory nodeTypeProviderFactory) : base(dependencies, sqlServerOptions)
        {
            _sqlServerOptions = sqlServerOptions;
            nodeTypeProviderFactory.RegisterMethods(WithNoLockExpressionNode.SupportedMethods,typeof(WithNoLockExpressionNode));
        }

        public override IQuerySqlGenerator CreateDefault(SelectExpression selectExpression)
        {
            if (selectExpression == null)
                throw new ArgumentNullException(nameof(selectExpression));
            return  new WithNoLockSqlServerQuerySqlGenerator(
                Dependencies,
             selectExpression,
                _sqlServerOptions.RowNumberPagingEnabled);
        }
    }
}
