using System;
using System.Collections.Generic;

namespace Calibration
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Нажмите Enter для начала калибровки или любую другую клавишу для выхода");
            TestReadKey();
            GenerateDevices generateDevices = new GenerateDevices();
            HashSet<Device> dev = generateDevices.GenerateDev(10);
            CalibrationInformation[] calibDev = generateDevices.GetDevInfoCalib(dev);
            Run(dev, calibDev);
            reStart(dev, calibDev);

        }
        /// <summary>
        /// Начало программы, вызоз калибровки или выход из приложения
        /// </summary>
        /// <param name="calib"></param>
        static void reStart(HashSet<Device> dev, CalibrationInformation[] calibDev)
        {
            Console.WriteLine("Желаете повторить калибровку? Нажмите Enter или любую другую клавишу для выхода");
            TestReadKey();
            Run(dev, calibDev);
            reStart(dev, calibDev);

            
        }
        public static void TestReadKey()
        {
            if (Console.ReadKey().Key != ConsoleKey.Enter)
            {
                Console.WriteLine("\nДо свидания!");
                Environment.Exit(0);
                Console.ReadKey();
            }
        }

        static void Run(HashSet<Device> dev, CalibrationInformation[] calibDev)
        {
            Examples example = new Examples();
            (double M, double E)[] arrayME = example.GeneratePairs();
            InstrumentCalibration calib = new InstrumentCalibration();
            calib.StartColibration(dev, calibDev, arrayME);
        }


    }
}
