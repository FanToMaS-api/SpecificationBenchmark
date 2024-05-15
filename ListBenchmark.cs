using BenchmarkDotNet.Attributes;

using Common.Specifications;

namespace SpecificationBenchmark
{
    public class ListBenchmark : IDisposable
    {
        private const int MinAge = 23;
        private const int MaxAge = 78;
        private const GenderType Gender = GenderType.Male;

        private readonly BadManRepository _badManRepository;
        private readonly ManRepository _manRepository;
        private readonly AppDbContext _appDbContext;

        private readonly ISpecification<Man> _manSpec;

        public ListBenchmark()
        {
            var dbContextFactory = new AppDbContextFactory();
            _appDbContext = dbContextFactory.CreateDbContext(Array.Empty<string>());

            _badManRepository = new(_appDbContext);
            _manRepository = new(_appDbContext);

            var spec = ManSpecification.CreateFullDataSpecification();
            spec &= ManSpecification.WithAgeGreaterOrEqual(MinAge);
            spec &= ManSpecification.WithAgeLessOrEqual(MaxAge);
            spec &= ManSpecification.WithGender(Gender);
            spec &= ManSpecification.OrderByName();

            _manSpec = spec;
        }

        public void Dispose()
        {
            _appDbContext.Dispose();
        }

        [Benchmark(Description = "List")]
        public Man[] List()
        {
            return _badManRepository.List(MinAge, MaxAge, Gender);
        }

        [Benchmark(Description = "List With Specifications")]
        public Man[] ListSpecification()
        {
            return _manRepository.GetMany(_manSpec).ToArray();
        }
    }
}
