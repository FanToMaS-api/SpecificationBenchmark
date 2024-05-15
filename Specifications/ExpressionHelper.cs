using System.Linq.Expressions;

namespace Common.Specifications
{
    /// <summary>
    ///     Помогает комбинировать выражения
    /// </summary>
    public static class ExpressionHelper
    {
        /// <summary>
        ///     Скомбинировать выражения через И
        /// </summary>
        public static Expression<T> And<T>(this Expression<T> left, Expression<T> right)
        {
            var param = left.Parameters[0];
            if (ReferenceEquals(param, right.Parameters[0]))
            {
                return Expression.Lambda<T>(
                        Expression.AndAlso(left.Body, right.Body),
                        param);
            }

            // оставляем левый как есть и вызываем правый, но с параметром для левого
            return Expression.Lambda<T>(
                Expression.AndAlso(
                    left.Body,
                    Expression.Invoke(right, param)),
                param);
        }

        /// <summary>
        ///     Скомбинировать выражения через ИЛИ
        /// </summary>
        public static Expression<T> Or<T>(this Expression<T> left, Expression<T> right)
        {
            var param = left.Parameters[0];
            if (ReferenceEquals(param, right.Parameters[0]))
            {
                return Expression.Lambda<T>(
                    Expression.OrElse(left, right),
                    param);
            }

            // оставляем левый как есть и вызываем правый, но с параметром для левого
            return Expression.Lambda<T>(
                Expression.OrElse(
                    left.Body,
                    Expression.Invoke(right, param)),
                param);
        }
    }
}
