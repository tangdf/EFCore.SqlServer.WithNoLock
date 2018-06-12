using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query.ResultOperators;
using Remotion.Linq;
using Remotion.Linq.Clauses;
using Remotion.Linq.Clauses.ResultOperators;
using Remotion.Linq.Clauses.StreamedData;

namespace Microsoft.EntityFrameworkCore.SqlServer.WithNoLock
{
    internal class WithNoLockResultOperator : SequenceTypePreservingResultOperatorBase, IQueryAnnotation
    {

        public virtual IQuerySource QuerySource { get; set; }


        public virtual QueryModel QueryModel { get; set; }


        public override string ToString() => "WithNoLock()";

        public override ResultOperatorBase Clone(CloneContext cloneContext) => new WithNoLockResultOperator();

        public override void TransformExpressions(Func<Expression, Expression> transformation)
        {
        }

        public override StreamedSequence ExecuteInMemory<T>(StreamedSequence input) => input;
    }
}
