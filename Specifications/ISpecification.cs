using System;
using System.Linq.Expressions;

namespace SpecificationBenchmark.Specifications
{
    /// <summary>
    ///     Базовый интерфейс спецификации
    /// </summary>
    public interface ISpecification<TEntity> : ICloneable
        where TEntity : class
    {
        /// <summary>
        ///     Сколько объектов пропустить
        /// </summary>
        int Skip { get; }

        /// <summary>
        ///     Сколько объектов взять
        /// </summary>
        int Take { get; }

        /// <summary>
        ///     Получить необходимые поля для включения
        /// </summary>
        Expression<Func<TEntity, object>>[] GetIncludes();

        /// <summary>
        ///     Удовлетворяет ли объект условиям
        /// </summary>
        Expression<Func<TEntity, bool>>? SatisfiedBy();

        /// <summary>
        ///     Получить модели для сортировки результатов
        /// </summary>
        OrderModel<TEntity>[] GetOrderModels();
    }
}
