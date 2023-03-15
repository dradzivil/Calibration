using System;
using System.Collections.Generic;

namespace Calibration
{
    internal class InstrumentCalibration
    {
        /// <summary>
        /// Массив приборов для примера
        /// </summary>
        static Device[] dev = new [] { 
            new Device ("A", "1a2sd09"), 
            new Device("B", "vi3f143"), 
            new Device("C", "ks8741c") 
        };
        /// <summary>
        /// Словарь с названием прибора и количеством колибровок
        /// </summary>
        public Dictionary<string, int> dict = new Dictionary<string, int>()
        {
            [dev[0].Name] = 0,
            [dev[1].Name] = 0,
            [dev[2].Name] = 0,
        };

        Random rand = new Random();
        // строки для вывода
        string str1 = "Для M = {0} и Е = {1} относительная погрешность = {2}%";
        string str2 = "Для Е = {0} и M = {1} поправочное значение К = {2}";

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
        /// <summary>
        /// Рассчет относительной погрешности для массива пар М Е
        /// </summary>
        /// <param name="arrayME"></param>
        /// <returns></returns>
        private double[] ArrayME((double M, double E)[] arrayME)
        {
            double[] arrayError = new double[arrayME.Length];
            for (int i = 0; i < arrayME.Length; i++)
            {
                arrayError[i] = Math.Abs(arrayME[i].M - arrayME[i].E) / arrayME[i].E * 100;
            }
            return arrayError;
        }
        /// <summary>
        /// Рассчет поправочного коэффицента К для пар значений М Е
        /// </summary>
        /// <param name="arrayME"></param>
        /// <returns></returns>
        private double[] CalibrationDevice((double M, double E)[] arrayME)
        {
            double[] arrayK = new double[arrayME.Length];
            for (int i = 0; i < arrayME.Length; i++)
            {
                arrayK[i] = arrayME[i].E - arrayME[i].M;
            }
            return arrayK;
        }
        /// <summary>
        /// Выбор типа прибора и вывод краткой информации
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        private string TypeOfDevice(Device[] array)
        {
            Console.WriteLine("Выберите тип прибора:");
            string name = Console.ReadLine();
            for (int i=0; i< dev.Length; i++)
            {
                if (dev[i].Name == name)
                {
                    Console.WriteLine(dev[i].Information());
                    return dev[i].Name;
                }
            }
            Console.WriteLine("Такого прибора нет!");
            return TypeOfDevice(array);
        }
        /// <summary>
        /// Печать на экран в консоль
        /// </summary>
        /// <param name="arrayME"></param>
        /// <param name="array"></param>
        /// <param name="str"></param>
        private void Print((double M, double E)[] arrayME, double[] array, string str)
        {
            for (int i = 0; i < arrayME.Length; i++)
            {
                Console.WriteLine(String.Format(str, arrayME[i].M, arrayME[i].E, array[i]));
            }
            Console.WriteLine("Где M - случайно измеренное значение, Е - эталонное значение\n");
        }
        /// <summary>
        /// Калибровка, пошаговый вызов нужных методов
        /// </summary>
        /// <param name="n"></param>
        public void StartColibration(int n)
        {
            // генерируем массив из кортежей - пар значений M и E
            (double M, double E)[] M_E = ArrayGeneration(n);
            // рассчитываем погрешность для каждой пары
            double[] Error = ArrayME(M_E);
            // выводим получившиеся значения
            Print(M_E, Error, str1);
            // узнаем тип прибора и выводим краткую информацию
            string deviceName = TypeOfDevice(dev);
            // рассчитываем поправочный К
            double[] K = CalibrationDevice(M_E);
            // выводим получившиеся значения
            Print(M_E, K, str2);
            //конец колибровки
            for (int i = 0; i< n ; i++)
            {
                Console.WriteLine(
                String.Format(
                    "M = {0} (с учетом поправочного К = {1}) " +
                    "и Е = {2} с относительной погрешностью {3}%", M_E[i].M + K[i], K[i], M_E[i].E, Error[i]));
            }
            Console.WriteLine("Где M - случайно измеренное значение, Е - эталонное значение\n");
            // увеличиваем количество колибровок прибора
            dict[deviceName]++;
            Console.WriteLine("Всего у прибора " + deviceName + " было произведено калибровок: " + dict[deviceName]);
        }
    }
}
