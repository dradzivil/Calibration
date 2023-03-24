using System;
using System.Collections.Generic;

namespace Calibration
{
    /// <summary>
    /// Генерация всего, что касается приборов
    /// </summary>
    internal class GenerateDevices
    {
        Random rand = new Random();

        /// <summary>
        /// Массив типов приборов
        /// </summary>
        static string[] names = new[] { "A", "B", "C" };

        /// <summary>
        /// Генерация множества приборов
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        internal HashSet<Device> GenerateDev(int number)
        {
            HashSet<Device> devices = new HashSet<Device>();
            Device[] dev = new Device[number];
            for (int i = 0; i < number; i++)
            {
                devices.Add(new Device(names[rand.Next(names.Length)], rand.Next(1, 10)));
                dev[i] = new Device(names[rand.Next(names.Length)], i);
            }
            return devices;
        }

        /// <summary>
        /// Генерация массива приборов с расширенной информацией о калибровке
        /// </summary>
        /// <param name="devices"></param>
        /// <returns></returns>
        internal CalibrationInformation[] GetDevInfoCalib(HashSet<Device> devices)
        {
            CalibrationInformation[] dev = new CalibrationInformation[devices.Count];
            int i = 0;
            foreach (Device device in devices)
            {
                dev[i] = new CalibrationInformation(device);
                i++;
            }
            return dev;
        }

        /// <summary>
        /// Генерация словаря для подсчета количества калибровок каждого типа
        /// </summary>
        /// <returns></returns>
        internal Dictionary<string, int> CountCalibTypeDev()
        {
            Dictionary<string, int> dictCount = new Dictionary<string, int>();
            foreach (string name in names)
            {
                dictCount.Add(name, 0);
            }
            return dictCount;
        }
    }
}
