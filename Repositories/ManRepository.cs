using SpecificationBenchmark.Database;
using SpecificationBenchmark.Models;
using SpecificationBenchmark.Specifications;

namespace SpecificationBenchmark.Repositories
{
    /// <summary>
    ///     Хранилище моделей людей
    /// </summary>
    internal sealed class ManRepository : StorageBase<Man>
    {
        private readonly AppDbContext _appDbContext;

        public ManRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        /// <inheritdoc />
        public override void Add(Man model)
        {
        }

        /// <inheritdoc />
        public override void Remove(Man model)
        {
        }

        /// <inheritdoc />
        protected override IEnumerable<Man> CreateQuery() => _appDbContext.Mans.AsQueryable();
    }
}
