using System.Linq.Expressions;

using SpecificationBenchmark.Helpers;

namespace SpecificationBenchmark.Specifications.Impl
{
    /// <summary>
    ///     Спецификация ИЛИ
    /// </summary>
    public class OrSpecification<TEntity> : SpecificationBase<TEntity>
        where TEntity : class
    {
        #region Fields

        private readonly ISpecification<TEntity> _leftSideSpecification;
        private readonly ISpecification<TEntity> _rightSideSpecification;

        #endregion

        #region .ctor

        /// <inheritdoc cref="OrSpecification{TEnity}" />
        public OrSpecification(
            ISpecification<TEntity> leftSide,
            ISpecification<TEntity> rightSide)
        {
            Assert.NotNull(leftSide, "Левая спецификация не может быть null");
            Assert.NotNull(rightSide, "Правая спецификация не может быть null");

            _leftSideSpecification = leftSide;
            _rightSideSpecification = rightSide;
        }

        #endregion

        #region Implemtation of SpecificationBase

        /// <inheritdoc />
        public override object Clone()
        {
            var left = (ISpecification<TEntity>)_leftSideSpecification.Clone();
            var right = (ISpecification<TEntity>)_rightSideSpecification.Clone();

            return new OrSpecification<TEntity>(left, right);
        }

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
                return left.Or(right);
            }

#pragma warning disable IDE0046 // Convert to conditional expression
            if (left is not null)
            {
                return left;
            }

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
