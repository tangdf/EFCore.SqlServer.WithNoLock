using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;

namespace Microsoft.EntityFrameworkCore.SqlServer.WithNoLock
{
    internal class WithNoLockMaterializerFactory: MaterializerFactory
    {
        private static readonly Func<SelectExpression, RelationalQueryCompilationContext> _getQueryCompilationContext;

        static WithNoLockMaterializerFactory()
        {
            _getQueryCompilationContext = Compile();
        }

        public WithNoLockMaterializerFactory(IEntityMaterializerSource entityMaterializerSource) : base(entityMaterializerSource)
        {
        }

        public override LambdaExpression CreateMaterializer(IEntityType entityType, SelectExpression selectExpression, Func<IProperty, SelectExpression, int> projectionAdder,
            out Dictionary<Type, int[]> typeIndexMap)
        {
            if (selectExpression == null)
                throw new ArgumentNullException(nameof(selectExpression));

            RelationalQueryCompilationContext queryCompilationContext =
                _getQueryCompilationContext(selectExpression);
            var isWithNoLock = queryCompilationContext.QueryAnnotations.OfType<WithNoLockResultOperator>().Any();
            if (isWithNoLock)
            {
                IEnumerable<TableExpression> tableExpressions = selectExpression.Tables.OfType<TableExpression>().ToArray();
                foreach (TableExpression tableExpression in tableExpressions)
                {
                    WithNoLockTableExpression withNoLockTableExpression =
                        new WithNoLockTableExpression(tableExpression.Table, tableExpression.Schema,
                            tableExpression.Alias, tableExpression.QuerySource);
                    selectExpression.RemoveTable(tableExpression);
                    selectExpression.AddTable(withNoLockTableExpression);
                }
            }


            return base.CreateMaterializer(entityType, selectExpression, projectionAdder, out typeIndexMap);
        }

        private static Func<SelectExpression, RelationalQueryCompilationContext> Compile()
        {
            var selectExpressionType = typeof(SelectExpression);

            var fieldInfo = selectExpressionType.GetTypeInfo().GetRuntimeFields()
                .Single(f => f.FieldType == typeof(RelationalQueryCompilationContext));

            var parameterExpression = Expression.Parameter(selectExpressionType, "selectExpression");

            var contextExpression = Expression.Field(parameterExpression, fieldInfo);

            return Expression
                .Lambda<Func<SelectExpression, RelationalQueryCompilationContext>>(contextExpression,
                    parameterExpression).Compile();

        }
    }
}
