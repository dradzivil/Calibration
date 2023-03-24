using System;
using System.Collections.Generic;

namespace Calibration
{
    /// <summary>
    /// Генерация примеров
    /// </summary>
    internal class Examples
    {
        Random rand = new Random();

        /// <summary>
        /// Ввод пользователем пар значений, вызов генерации массива полученного количества пар значений
        /// </summary>
        /// <returns></returns>
        internal (double M, double E)[] GeneratePairs()
        {
            Console.WriteLine("\nВведите количество пар значений М Е: ");
            try
            {
                int n = Convert.ToInt32(Console.ReadLine().Trim());
                return ArrayGeneration(n);
            }
            catch
            {
                Console.WriteLine("Введите корректное число!");
                return GeneratePairs();
            }
        }

        /// <summary>
        /// Генерация массива размера n из кортежей - пар значений M и E
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private (double M, double E)[] ArrayGeneration(int n)
        {
            (double M, double E)[] array_M_E = new (double M, double E)[n];
            for (int i = 0; i < n; i++)
            {
                array_M_E[i] = (((double)rand.Next(1, 10000) / 1000), ((double)rand.Next(1, 10000) / 1000));
            }
            return array_M_E;
        }
    }
}
