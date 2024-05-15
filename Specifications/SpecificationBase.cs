using System;
using System.Linq.Expressions;

using Common.Specifications.Impl;

namespace SpecificationBenchmark.Specifications
{
    /// <summary>
    ///     Базовая спецификация для коллекций объектов
    /// </summary>
    public abstract class SpecificationBase<TEntity> : ISpecification<TEntity>
        where TEntity : class
    {
        #region Implementation of ISpecification

        /// <inheritdoc />
        public int Skip { get; set; } = 0;

        /// <inheritdoc />
        public int Take { get; set; } = int.MaxValue;

        /// <inheritdoc />
        public abstract Expression<Func<TEntity, bool>>? SatisfiedBy();

        /// <inheritdoc />
        public abstract Expression<Func<TEntity, object>>[] GetIncludes();

        /// <inheritdoc />
        public abstract OrderModel<TEntity>[] GetOrderModels();

        /// <inheritdoc />
        public abstract object Clone();

        #endregion

        /// <summary>
        ///     Перегрузка оператора И
        /// </summary>
        public static SpecificationBase<TEntity> operator &(
            SpecificationBase<TEntity> left,
            SpecificationBase<TEntity> right)
        {
            return new AndSpecification<TEntity>(left, right);
        }

        /// <summary>
        ///     Перегрузка оператора ИЛИ
        /// </summary>
        public static SpecificationBase<TEntity> operator |(
            SpecificationBase<TEntity> left,
            SpecificationBase<TEntity> right)
        {
            return new OrSpecification<TEntity>(left, right);
        }
    }
}
