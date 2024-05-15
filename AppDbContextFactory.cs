using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SpecificationBenchmark
{
    /// <summary>
    ///     Design-time фабрика для <see cref="AppDbContext"/>
    /// </summary>
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        /// <inheritdoc />
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql("Server=localhost;UserID=postgres;Port=5432;Database=dvdrental;Password=postgres;Include Error Detail=true");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
