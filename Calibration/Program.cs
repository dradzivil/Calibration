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
            CalibrationInformation[] calibDev = generateDevices.GetDevInfoCalib(generateDevices.GenerateDev(10));
            Dictionary<string, int> dictCount = generateDevices.CountCalibTypeDev();
            
            Run(calibDev, dictCount);
            reStart(calibDev, dictCount);
        }
        /// <summary>
        /// Перезапуск калибровки или выход из программы
        /// </summary>
        /// <param name="calibDev"></param>
        /// <param name="dictCount"></param>
        static void reStart(CalibrationInformation[] calibDev, Dictionary<string, int> dictCount)
        {
            Console.WriteLine("\nЖелаете повторить калибровку? Нажмите Enter или любую другую клавишу для выхода");
            TestReadKey();
            
            Run(calibDev, dictCount);
            reStart(calibDev, dictCount);
        }
        /// <summary>
        /// Проверяет ввод с клавиатуры, завершает программу
        /// </summary>
        public static void TestReadKey()
        {
            if (Console.ReadKey().Key != ConsoleKey.Enter)
            {
                Console.WriteLine("\nДо свидания!");
                Environment.Exit(0);
                Console.ReadKey();
            }
        }
        /// <summary>
        /// Запускает калибровку, передает ранее сгенерированные приборы и количество калибровок каждого типа
        /// </summary>
        /// <param name="calibDev"></param>
        /// <param name="dictCount"></param>
        static void Run(CalibrationInformation[] calibDev, Dictionary<string, int> dictCount)
        {
            Examples example = new Examples();
            (double M, double E)[] arrayME = example.GeneratePairs();
            InstrumentCalibration calib = new InstrumentCalibration();
            
            calib.StartColibration(calibDev, arrayME, dictCount);
        }


    }
}
