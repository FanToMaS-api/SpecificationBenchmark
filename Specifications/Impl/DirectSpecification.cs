using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using SpecificationBenchmark.Specifications;

namespace SpecificationBenchmark.Specifications.Impl
{
    /// <summary>
    ///     Прямая спецификация
    /// </summary>
    public class DirectSpecification<TEntity> : SpecificationBase<TEntity>
        where TEntity : class
    {
        #region Fields

        private readonly List<Expression<Func<TEntity, object>>> _includes = new();
        private readonly Expression<Func<TEntity, bool>>? _matchingCriteria;
        private OrderModel<TEntity>? _orderModel;

        #endregion

        #region .ctor

        /// <inheritdoc cref="DirectSpecification{TEntity}" />
        public DirectSpecification(Expression<Func<TEntity, bool>> matchingCriteria)
        {
            _matchingCriteria = matchingCriteria;
        }

        /// <inheritdoc cref="DirectSpecification{TEntity}" />
        public DirectSpecification()
        { }

        /// <inheritdoc cref="DirectSpecification{TEntity}" />
        protected DirectSpecification(
            List<Expression<Func<TEntity, object>>> includes,
            Expression<Func<TEntity, bool>>? matchingCriteria,
            OrderModel<TEntity>? orderModel)
        {
            _includes = includes;
            _matchingCriteria = matchingCriteria;
            _orderModel = orderModel;
        }

        #endregion

        #region Implementation of SpecificationBase

        /// <inheritdoc />
        public override object Clone()
        {
            // NOTE: поскольку список не смотрит из объекта явно,
            // то нет необходимости перекопировать его полностью включая внутренние элементы
            // аналогично и с моделью сортировки, считается, что она неизменяемая
            return new DirectSpecification<TEntity>(_includes, _matchingCriteria, _orderModel);
        }

        /// <inheritdoc />
        public override Expression<Func<TEntity, bool>>? SatisfiedBy()
            => _matchingCriteria;

        /// <inheritdoc />
        public override Expression<Func<TEntity, object>>[] GetIncludes()
            => _includes.ToArray();

        /// <inheritdoc />
        public override OrderModel<TEntity>[] GetOrderModels()
        {
            return _orderModel is null ? Array.Empty<OrderModel<TEntity>>() : new[] { _orderModel };
        }

        #endregion

        #region Public methods

        /// <summary>
        ///     Добавить включение
        /// </summary>
        public DirectSpecification<TEntity> AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            _includes.Add(includeExpression);

            return this;
        }

        /// <summary>
        ///     Установить модель сортировки
        /// </summary>
        public DirectSpecification<TEntity> SetOrder(OrderModel<TEntity> orderModel)
        {
            _orderModel = orderModel;

            return this;
        }

        #endregion
    }
}
