using BenchmarkDotNet.Running;

using Bogus;

using Microsoft.EntityFrameworkCore;

using SpecificationBenchmark;
using SpecificationBenchmark.Database;
using SpecificationBenchmark.Models;

try
{
    var dbContextFactory = new AppDbContextFactory();
    using var database = dbContextFactory.CreateDbContext(Array.Empty<string>());

    database.Database.Migrate();

    GenerateData(database);

    BenchmarkRunner.Run<ListBenchmark>();
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}

static void GenerateData(AppDbContext database)
{
    database.Database.ExecuteSqlRaw("TRUNCATE TABLE mans");

    var testUsers = new Faker<Man>()
        .RuleFor(u => u.Name, f => f.Name.FirstName())
        .RuleFor(u => u.Gender, f => f.PickRandom<GenderType>())
        .RuleFor(p => p.Age, f => f.Random.Number(8, 90));

    using var transaction = database.Database.BeginTransaction();

    var count = 200_000; // 200 000 записей
    for (var i = 0; i < count; i++)
    {
        var man = testUsers.Generate();

        database.Mans.Add(man);
    }

    database.SaveChanges();

    transaction.Commit();
}
