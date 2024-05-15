using Microsoft.EntityFrameworkCore;

using SpecificationBenchmark.Database;
using SpecificationBenchmark.Models;

namespace SpecificationBenchmark.Repositories
{
    /// <summary>
    ///     Неоптимальное хранилище моделей людей
    /// </summary>
    internal sealed class BadManRepository
    {
        private readonly AppDbContext _appDbContext;
        public BadManRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        /// <inheritdoc />
        public Man[] List(int minAge, int maxAge, GenderType genderType)
        {
            var query = _appDbContext.Mans.AsNoTracking();

            query = query.Where(_ => _.Age >= minAge && _.Age <= maxAge && _.Gender == genderType);
            query = query.OrderBy(_ => _.Name);

            return query.ToArray();
        }
    }
}
