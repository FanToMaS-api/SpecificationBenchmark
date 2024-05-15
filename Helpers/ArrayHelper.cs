using System;

namespace SpecificationBenchmark.Helpers
{
    /// <summary>
    ///     Помогает в работе с массивами
    /// </summary>
    public static class ArrayHelper
    {
        /// <summary>
        ///     Объединяет два массива в один новый
        /// </summary>
        /// <typeparam name="T"> Тип массивов </typeparam>
        /// <param name="array1"> Первый массив </param>
        /// <param name="array2"> Второй массив </param>
        /// <returns> Новый массив со значениями из первого и второго </returns>
        public static T[] AddRange<T>(this T[] array1, T[] array2)
        {
            var newArray = new T[array1.Length + array2.Length];
            Array.Copy(array1, newArray, array1.Length);
            Array.Copy(array2, 0, newArray, array1.Length, array2.Length);

            return newArray;
        }
    }
}
