namespace SpecificationBenchmark.Specifications
{
    /// <summary>
    ///     Общий набор методов для всех хранилищ
    /// </summary>
    public abstract class StorageBase<T> : IStorage<T>
        where T : class
    {
        #region Public methods

        /// <inheritdoc />
        public abstract void Add(T model);

        /// <inheritdoc />
        public abstract void Remove(T model);

        /// <inheritdoc />
        public bool Any(ISpecification<T> specification)
            => SpecificationEvaluator.GetQuery(CreateQuery(), specification).Any();

        /// <inheritdoc />
        public T? Get(ISpecification<T> specification)
            => SpecificationEvaluator.GetQuery(CreateQuery(), specification).FirstOrDefault();

        /// <inheritdoc />
        public T GetStrict(ISpecification<T> specification, string errorCode)
            => Get(specification) ?? throw new Exception(errorCode);

        /// <inheritdoc />
        public IEnumerable<T> GetMany(ISpecification<T> specification)
            => SpecificationEvaluator.GetQuery(CreateQuery(), specification);

        #endregion

        #region Protected methods

        /// <summary>
        ///     Создать запрос к таблице с <typeparamref name="T" />
        /// </summary>
        protected abstract IEnumerable<T> CreateQuery();

        #endregion
    }
}
