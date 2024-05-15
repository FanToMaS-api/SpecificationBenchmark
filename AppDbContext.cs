using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

using Npgsql.NameTranslation;

namespace SpecificationBenchmark
{
    /// <summary>
    ///     База данных приложения
    /// </summary>
    public class AppDbContext : DbContext
    {
        #region .ctor

        /// <inheritdoc cref="AppDbContext" />
        public AppDbContext(DbContextOptions options) : base(options) { }

        #endregion

        #region Tables

        /// <inheritdoc cref="Man"/>
        public DbSet<Man> Mans { get; set; }

        #endregion

        #region Public methods

        /// <inheritdoc />
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Проставляем имя поля по умолчанию (snake_case)
            var mapper = new NpgsqlSnakeCaseNameTranslator();
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var storeObjectId = StoreObjectIdentifier.Table(entity.GetTableName(), entity.GetSchema());
                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(mapper.TranslateMemberName(property.GetColumnName(storeObjectId)));
                }
            }

            // Setup
            Man.Setup(modelBuilder.Entity<Man>());
        }

        #endregion
    }
}
