using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Resolver.QueryableExtensions
{
    public static class QueryableFilterExtensions
    {
        public static IQueryable<T> OrderByPropertyOrField<T>
        (this IQueryable<T> queryable, string propertyOrFieldName, string ascending)
        {
            var elementType = typeof(T);
            var orderByMethodName = ascending.Equals("desc") ? "OrderByDescending" : "OrderBy";

            var parameterExpression = Expression.Parameter(elementType);
            var propertyOrFieldExpression =
                Expression.PropertyOrField(parameterExpression, propertyOrFieldName);
            var selector = Expression.Lambda(propertyOrFieldExpression, parameterExpression);

            var orderByExpression = Expression.Call(typeof(Queryable), orderByMethodName,
                new[] { elementType, propertyOrFieldExpression.Type }, queryable.Expression, selector);

            return queryable.Provider.CreateQuery<T>(orderByExpression);
        }
    }
}
