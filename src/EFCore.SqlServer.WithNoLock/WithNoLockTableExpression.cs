using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Remotion.Linq.Clauses;

namespace Microsoft.EntityFrameworkCore.SqlServer.WithNoLock
{
    internal class WithNoLockTableExpression: TableExpression
    {
        public WithNoLockTableExpression(string table, string schema, string alias, IQuerySource querySource) : base(table, schema, alias, querySource)
        {
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == GetType() && Equals((WithNoLockTableExpression)obj);
        }

        private bool Equals(WithNoLockTableExpression other)
            => string.Equals(Table, other.Table)
               && string.Equals(Schema, other.Schema)
               && string.Equals(Alias, other.Alias)
               && Equals(QuerySource, other.QuerySource);

        /// <summary>
        ///     Returns a hash code for this object.
        /// </summary>
        /// <returns>
        ///     A hash code for this object.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ this.GetType().GetHashCode();
            }
        }
        public override string ToString() => Table + " " + Alias +" With(nolock)";
    }
}
