using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common.Specifications.Impl;

using SpecificationBenchmark.Models;
using SpecificationBenchmark.Specifications;

namespace SpecificationBenchmark
{
    /// <summary>
    ///     Статический класс для создания спецификации для получения <see cref="Man" />
    /// </summary>
    internal static class ManSpecification
    {
        /// <summary>
        ///     Спецификация для возврата всех данных
        /// </summary>
        public static SpecificationBase<Man> CreateFullDataSpecification()
            => new FullDataSpecification<Man>();

        /// <summary>
        ///     С именем
        /// </summary>
        public static SpecificationBase<Man> WithName(string name)
            => new DirectSpecification<Man>(_ => _.Name == name);

        /// <summary>
        ///     С возрастом
        /// </summary>
        public static SpecificationBase<Man> WithAge(int age)
            => new DirectSpecification<Man>(_ => _.Age == age);

        /// <summary>
        ///     С возрастом больше или равным
        /// </summary>
        public static SpecificationBase<Man> WithAgeGreaterOrEqual(int age)
            => new DirectSpecification<Man>(_ => _.Age >= age);

        /// <summary>
        ///     С возрастом меньше или равным
        /// </summary>
        public static SpecificationBase<Man> WithAgeLessOrEqual(int age)
            => new DirectSpecification<Man>(_ => _.Age <= age);

        /// <summary>
        ///     С гендером
        /// </summary>
        public static SpecificationBase<Man> WithGender(GenderType gender)
            => new DirectSpecification<Man>(_ => _.Gender == gender);

        /// <summary>
        ///     Сортировать по возрасту
        /// </summary>
        public static SpecificationBase<Man> OrderByAge(bool orderByDescending = false)
            => new DirectSpecification<Man>().SetOrder(new(static _ => _.Age, orderByDescending));

        /// <summary>
        ///     Сортировать по имени
        /// </summary>
        public static SpecificationBase<Man> OrderByName(bool orderByDescending = false)
            => new DirectSpecification<Man>().SetOrder(new(static _ => _.Name, orderByDescending));
    }
}
