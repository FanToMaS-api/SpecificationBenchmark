using System;
using System.Linq.Expressions;

namespace Common.Specifications
{
    /// <summary>
    ///     Модель для хранения сортирующего выражения
    /// </summary>
    public class OrderModel<TEntity>
        where TEntity : class
    {
        #region .ctor

        /// <inheritdoc cref="OrderModel{TEntity}" />
        public OrderModel(Expression<Func<TEntity, object>> orderExpression, bool needOrderByDescending)
        {
            OrderExpression = orderExpression;
            NeedOrderByDescending = needOrderByDescending;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Сортирующее выражение
        /// </summary>
        public Expression<Func<TEntity, object>> OrderExpression { get; }

        /// <summary>
        ///     Нужна ли сортировка по убыванию
        /// </summary>
        public bool NeedOrderByDescending { get; }

        #endregion
    }
}
