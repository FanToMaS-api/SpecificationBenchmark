using System;
using System.Linq.Expressions;

namespace SpecificationBenchmark.Specifications.Impl
{
    /// <summary>
    ///     Пустая спецификация не возвращающая никакие данные
    /// </summary>
    public class EmptySpecification<TEntity> : SpecificationBase<TEntity>
        where TEntity : class
    {
        /// <inheritdoc />
        public override object Clone() => new EmptySpecification<TEntity>();

        /// <inheritdoc />
        public override Expression<Func<TEntity, bool>>? SatisfiedBy()
            => static _ => false;

        /// <inheritdoc />
        public override Expression<Func<TEntity, object>>[] GetIncludes()
            => Array.Empty<Expression<Func<TEntity, object>>>();

        /// <inheritdoc />
        public override OrderModel<TEntity>[] GetOrderModels()
            => Array.Empty<OrderModel<TEntity>>();
    }
}
