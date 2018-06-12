using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Remotion.Linq.Clauses;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace Microsoft.EntityFrameworkCore.SqlServer.WithNoLock
{
    internal class WithNoLockExpressionNode : ResultOperatorExpressionNodeBase
    {
        public static readonly IReadOnlyCollection<MethodInfo> SupportedMethods =
            new[] {
                EntityFrameworkQueryableExtensions.WithNoLockMethodInfo
            };

        public WithNoLockExpressionNode(MethodCallExpressionParseInfo parseInfo) : base(parseInfo, null, null)
        {
        }


        protected override ResultOperatorBase CreateResultOperator(ClauseGenerationContext clauseGenerationContext) =>
            new WithNoLockResultOperator();


        public override Expression Resolve(ParameterExpression inputParameter, Expression expressionToBeResolved,
            ClauseGenerationContext clauseGenerationContext) =>
            Source.Resolve(inputParameter, expressionToBeResolved, clauseGenerationContext);
    }
}

 