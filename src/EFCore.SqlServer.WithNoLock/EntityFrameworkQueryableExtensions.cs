using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Microsoft.EntityFrameworkCore.SqlServer.WithNoLock
{
    public static class EntityFrameworkQueryableExtensions
    {
        internal static readonly MethodInfo WithNoLockMethodInfo
            = typeof(EntityFrameworkQueryableExtensions)
                .GetTypeInfo().GetDeclaredMethods(nameof(WithNoLock))
                .Single();

        public static IQueryable<TEntity> WithNoLock<TEntity>(
           this IQueryable<TEntity> source)
            where TEntity : class
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return
                source.Provider is EntityQueryProvider
                    ? source.Provider.CreateQuery<TEntity>(
                        Expression.Call(
                            instance: null,
                            method: WithNoLockMethodInfo.MakeGenericMethod(typeof(TEntity)),
                            arguments: source.Expression))
                    : source;
        }
     
    }
}
