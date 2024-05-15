using System;

namespace SpecificationBenchmark.Helpers
{
    /// <summary>
    ///     Проверяет утверждения
    /// </summary>
    public static class Assert
    {
        /// <summary>
        ///     Проверяет объект на null
        /// </summary>
        public static void NotNull<T>(T? obj, string message)
        {
            ArgumentNullException.ThrowIfNull(obj, $"Ошибка! Объект = null. {message}");
        }

        /// <summary>
        ///     Проверяет утверждение на истинность
        /// </summary>
        public static void True(bool condition, string message)
        {
            if (!condition)
            {
                throw new Exception($"Ошибка! Сравнение ложно. {message}");
            }
        }

        /// <summary>
        ///     Проверяет утверждение на ложь
        /// </summary>
        public static void False(bool condition, string message)
        {
            if (condition)
            {
                throw new Exception($"Ошибка! Сравнение истинно. {message}");
            }
        }
    }
}
