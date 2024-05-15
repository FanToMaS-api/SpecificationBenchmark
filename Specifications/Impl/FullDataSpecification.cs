using System;
using System.Linq.Expressions;

namespace SpecificationBenchmark.Specifications.Impl
{
    /// <summary>
    ///     Пустая спецификация, удовлетворяющая любым условиям
    /// </summary>
    /// <remarks>
    ///     Всегда возвращает все данные
    /// </remarks>
    public class FullDataSpecification<TEntity> : SpecificationBase<TEntity>
        where TEntity : class
    {
        /// <inheritdoc />
        public override object Clone() => new FullDataSpecification<TEntity>();

        /// <inheritdoc />
        public override Expression<Func<TEntity, bool>>? SatisfiedBy()
            => static _ => true;

        /// <inheritdoc />
        public override Expression<Func<TEntity, object>>[] GetIncludes()
            => Array.Empty<Expression<Func<TEntity, object>>>();

        /// <inheritdoc />
        public override OrderModel<TEntity>[] GetOrderModels()
            => Array.Empty<OrderModel<TEntity>>();
    }
}
