using System;
using System.Linq.Expressions;

using Common.Helpers;

namespace Common.Specifications.Impl
{
    /// <summary>
    ///     Спецификация И
    /// </summary>
    public sealed class AndSpecification<TEntity> : SpecificationBase<TEntity>
       where TEntity : class
    {
        #region Fields

        private readonly ISpecification<TEntity> _rightSideSpecification;
        private readonly ISpecification<TEntity> _leftSideSpecification;

        #endregion

        #region .ctor

        /// <inheritdoc />
        public override object Clone()
        {
            var left = (ISpecification<TEntity>)_leftSideSpecification.Clone();
            var right = (ISpecification<TEntity>)_rightSideSpecification.Clone();

            return new AndSpecification<TEntity>(left, right);
        }

        /// <inheritdoc cref="AndSpecification{TEnity}" />
        public AndSpecification(
            ISpecification<TEntity> leftSide,
            ISpecification<TEntity> rightSide)
        {
            Assert.NotNull(leftSide, "Левая спецификация не может быть null");
            Assert.NotNull(rightSide, "Правая спецификация не может быть null");

            _leftSideSpecification = leftSide;
            _rightSideSpecification = rightSide;
        }

        #endregion

        #region Implementation Of SpecificationBase

        /// <inheritdoc />
        public override Expression<Func<TEntity, bool>>? SatisfiedBy()
        {
            var left = _leftSideSpecification.SatisfiedBy();
            var right = _rightSideSpecification.SatisfiedBy();
            if (left is null && right is null)
            {
                return null;
            }

            if (left is not null && right is not null)
            {
                return left.And(right);
            }

#pragma warning disable IDE0046 // Convert to conditional expression
            if (left is not null)
            {
                return left;
            }
#pragma warning restore IDE0046 // Convert to conditional expression

            return right;
        }

        /// <inheritdoc />
        public override Expression<Func<TEntity, object>>[] GetIncludes()
        {
            var leftIncludes = _leftSideSpecification.GetIncludes();
            var rightIncludes = _rightSideSpecification.GetIncludes();

            leftIncludes.AddRange(rightIncludes);

            return leftIncludes;
        }

        /// <inheritdoc />
        public override OrderModel<TEntity>[] GetOrderModels()
        {
            var leftOrderModels = _leftSideSpecification.GetOrderModels();
            leftOrderModels.AddRange(_rightSideSpecification.GetOrderModels());

            return leftOrderModels;
        }

        #endregion
    }
}
