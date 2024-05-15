using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SpecificationBenchmark
{
    /// <summary>
    ///     Человек
    /// </summary>
    [Table("mans")]
    public sealed class Man
    {
        /// <summary>
        ///     Уникальный идентификатор
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        ///     Возраст
        /// </summary>
        [Column("age")]
        public int Age { get; set; }

        /// <summary>
        ///     Имя
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        ///     Пол
        /// </summary>
        [Column("gender")]
        public GenderType Gender { get; set; }

        /// <summary>
        ///     Настройка
        /// </summary>
        public static void Setup(EntityTypeBuilder<Man> builder)
        {
            // Индексы
            builder.HasIndex(_ => _.Name).HasDatabaseName("IX_mans_name");
            builder.HasIndex(_ => _.Age).HasDatabaseName("IX_mans_age");
            builder.HasIndex(_ => _.Gender).HasDatabaseName("IX_mans_gender");
        }
    }
}
