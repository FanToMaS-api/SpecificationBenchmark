using Common.Specifications;

namespace SpecificationBenchmark.Specifications
{
    /// <summary>
    ///     Общий интерфейс хранилищ
    /// </summary>
    public interface IStorage<T>
        where T : class
    {
        /// <summary>
        ///     Добавляет новую модель в хранилище
        /// </summary>
        void Add(T model);

        /// <summary>
        ///     Удалить
        /// </summary>
        void Remove(T model);

        /// <summary>
        ///     Находит модель по идентификатору
        /// </summary>
        /// <param name="specification"> Спецификация получения данных </param>
        /// <returns> Модель </returns>
        T? Get(ISpecification<T> specification);

        /// <summary>
        ///     Находит модель по идентификатору, бросает ошибку, если не найдено
        /// </summary>
        /// <param name="specification"> Спецификация получения данных </param>
        /// <param name="errorCode"> Код ошибки, если модель не найдена </param>
        /// <returns> Модель </returns>
        /// <exception cref="ErrorException">
        ///     Ошибка с кодом <paramref name="errorCode" />, если модель не найдена
        /// </exception>
        T GetStrict(ISpecification<T> specification, string errorCode);

        /// <summary>
        ///     Определяет соответствуют ли выбранные объекты условиям спецификации
        /// </summary>
        /// <param name="specification"> Спецификация </param>
        bool Any(ISpecification<T> specification);

        /// <summary>
        ///     Получить сущности
        /// </summary>
        /// <param name="specification"> Спецификация </param>
        IEnumerable<T> GetMany(ISpecification<T> specification);
    }
}
