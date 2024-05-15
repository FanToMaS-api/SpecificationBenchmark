using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace SpecificationBenchmark.Specifications
{
    /// <summary>
    ///     Создает запрос к базе данных на основе спецификации
    /// </summary>
    public class SpecificationEvaluator
    {
        #region IQueryable

        /// <summary>
        ///     Получить сформированный запрос
        /// </summary>
        public static IQueryable<TEntity> GetQuery<TEntity>(
            IQueryable<TEntity> inputQuery,
            ISpecification<TEntity> specification)
            where TEntity : class
        {
            var query = inputQuery;
            query = specification
                .GetIncludes()
                .Aggregate(query, static (current, include) => current.Include(include));

            var whereExp = specification.SatisfiedBy();
            if (whereExp is not null)
            {
                query = query.Where(whereExp)!;
            }

            var orderModels = specification.GetOrderModels();
            if (!orderModels.Any())
            {
                return query
                    .Skip(specification.Skip)
                    .Take(specification.Take);
            }

            var orderedQuery = AddFirstOrderExpression(query, orderModels.First());
            foreach (var orderModel in orderModels.Skip(1))
            {
                orderedQuery = AddAnotherOrderExpression(orderedQuery, orderModel);
            }

            return orderedQuery
                .Skip(specification.Skip)
                .Take(specification.Take);
        }

        /// <summary>
        ///     Добавить сортировку в самый первый раз
        /// </summary>
        private static IOrderedQueryable<TEntity> AddFirstOrderExpression<TEntity>(
            IQueryable<TEntity> query,
            OrderModel<TEntity> orderModel)
            where TEntity : class
        {
            return orderModel.NeedOrderByDescending
                ? query.OrderByDescending(orderModel.OrderExpression)
                : query.OrderBy(orderModel.OrderExpression);
        }

        /// <summary>
        ///     Продолжить добавление сортировок
        /// </summary>
        private static IOrderedQueryable<TEntity> AddAnotherOrderExpression<TEntity>(
            IOrderedQueryable<TEntity> query,
            OrderModel<TEntity> orderModel)
            where TEntity : class
        {
            return orderModel.NeedOrderByDescending
                ? query.ThenByDescending(orderModel.OrderExpression)
                : query.ThenBy(orderModel.OrderExpression);
        }

        #endregion

        #region IEnumerable

        /// <summary>
        ///     Получить сформированный запрос
        /// </summary>
        public static IEnumerable<TEntity> GetQuery<TEntity>(
            IEnumerable<TEntity> inputQuery,
            ISpecification<TEntity> specification)
            where TEntity : class
        {
            var query = inputQuery;
            var whereExp = specification.SatisfiedBy();
            if (whereExp is not null)
            {
                query = query.Where(whereExp.Compile());
            }

            var orderModels = specification.GetOrderModels();
            if (!orderModels.Any())
            {
                return query
                    .Skip(specification.Skip)
                    .Take(specification.Take);
            }

            var orderedQuery = AddFirstOrderExpression(query, orderModels.First());
            foreach (var orderModel in orderModels.Skip(1))
            {
                orderedQuery = AddAnotherOrderExpression(orderedQuery, orderModel);
            }

            return orderedQuery
                .Skip(specification.Skip)
                .Take(specification.Take);
        }

        /// <summary>
        ///     Добавить сортировку в самый первый раз
        /// </summary>
        private static IOrderedEnumerable<TEntity> AddFirstOrderExpression<TEntity>(
            IEnumerable<TEntity> query,
            OrderModel<TEntity> orderModel)
            where TEntity : class
        {
            return orderModel.NeedOrderByDescending
                ? query.OrderByDescending(orderModel.OrderExpression.Compile())
                : query.OrderBy(orderModel.OrderExpression.Compile());
        }

        /// <summary>
        ///     Продолжить добавление сортировок
        /// </summary>
        private static IOrderedEnumerable<TEntity> AddAnotherOrderExpression<TEntity>(
            IOrderedEnumerable<TEntity> query,
            OrderModel<TEntity> orderModel)
            where TEntity : class
        {
            return orderModel.NeedOrderByDescending
                ? query.ThenByDescending(orderModel.OrderExpression.Compile())
                : query.ThenBy(orderModel.OrderExpression.Compile());
        }

        #endregion
    }
}
