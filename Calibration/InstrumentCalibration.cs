using System;
using System.Collections.Generic;

namespace Calibration
{
    /// <summary>
    /// Класс для подсчетов
    /// </summary>
    internal class InstrumentCalibration
    {
        Random rand = new Random();
        // строки для вывода
        string str1 = "Для M = {0:0.000} и Е = {1:0.000} относительная погрешность = {2:0.000}%";
        string str2 = "Для Е = {0:0.000} и M = {1:0.000} поправочное значение К = {2:0.000}";
        
        /// <summary>
        /// Перебор массива, подсчет по поступаемой в качестве аргумента функции
        /// </summary>
        /// <param name="arrayME"></param>
        /// <param name="myMethodCount"></param>
        /// <returns></returns>
        private double[] Array((double M, double E)[] arrayME, Func<(double M, double E), double> myMethodCount)
        {
            double[] array = new double[arrayME.Length];
            for (int i = 0; i < arrayME.Length; i++)
            {
                array[i] = myMethodCount(arrayME[i]);
            }
            return array;
        }

        /// <summary>
        /// Функция рассчета относительной погрешности
        /// </summary>
        /// <param name="ME"></param>
        /// <returns></returns>
        private double CountError((double M, double E) ME) => Math.Abs(ME.M - ME.E) / ME.E * 100;


        /// <summary>
        /// Выбор прибора для калибрации из предложенных
        /// </summary>
        /// <param name="calibDev"></param>
        /// <returns></returns>
        private CalibrationInformation TypeOfDevice(CalibrationInformation[] calibDev)
        {
            Console.WriteLine("Выберите тип прибора из предложенных:");
            HashSet<string> chooseDev = new HashSet<string>(); ;
            while (chooseDev.Count <3)
            { 
                chooseDev.Add(calibDev[rand.Next(calibDev.Length)].FullName);
            }
            foreach (string device in chooseDev)
            {
                Console.Write(device + ' ');
            }

            string name = Console.ReadLine().Trim();
            if (chooseDev.Contains(name))
            {
                foreach (CalibrationInformation dev in calibDev)
                {
                    if (dev.FullName == name)
                    {
                        Console.WriteLine("\n" + dev.Device.Information());
                        return dev;
                    }
                }
            }
            Console.WriteLine("Выберите прибор из списка!");
            return TypeOfDevice(calibDev);
        }

        /// <summary>
        /// Вывод в консоль пар значений и рассчитанного массива значений
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
        /// Подсчет количества калибровок данного прибора и всех приборов данного типа
        /// </summary>
        /// <param name="choosenDevice"></param>
        /// <param name="dictCount"></param>
        private void CountCalibration(CalibrationInformation choosenDevice, Dictionary<string, int> dictCount)
        {
            choosenDevice.NumberColibration++;
            dictCount[choosenDevice.Device.Name]++;
            Console.WriteLine("Всего у прибора " + choosenDevice.FullName + " было произведено калибровок: "
                + choosenDevice.NumberColibration + ".\nОбщее количество колибровок приборов типа " + choosenDevice.Device.Name 
                + " : " + dictCount[choosenDevice.Device.Name]);
        }

        /// <summary>
        /// Основная логика подсчета, вызов функций. Конечный итог - выводы рассчетов по выбранному прибору
        /// </summary>
        /// <param name="calibDev"></param>
        /// <param name="arrayME"></param>
        /// <param name="dictCount"></param>
        public void StartColibration(CalibrationInformation[] calibDev, (double M, double E)[] arrayME, Dictionary<string, int> dictCount)
        {
            // считаем погрешность
            double[] Error = Array(arrayME, CountError);
            // выводим
            Print(arrayME, Error, str1);
            // выбираем прибор
            CalibrationInformation choosenDevice = TypeOfDevice(calibDev);
            // рассчитываем поправочный коэффицент
            choosenDevice.ArrayK = Array(arrayME, choosenDevice.CountK);
            // выводим
            Print(arrayME, choosenDevice.ArrayK, str2);
            // итог по подсчетам
            for (int i = 0; i< arrayME.Length; i++)
            {
                Console.WriteLine(
                String.Format(
                    "M = {0:0.000} (с учетом поправочного К = {1:0.000}) " +
                    "и Е = {2:0.000} с относительной погрешностью {3:0.000}%", 
                    arrayME[i].M + choosenDevice.ArrayK[i], choosenDevice.ArrayK[i], arrayME[i].E, Error[i]));
            }
            Console.WriteLine("Где M - случайно измеренное значение, Е - эталонное значение\n");
            // вывод количества калибровок
            CountCalibration(choosenDevice, dictCount);
        }
    }
}