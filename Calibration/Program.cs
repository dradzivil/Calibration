using System;

namespace Calibration
{
    internal class Program
    {
        static void Main(string[] args)
        {
            InstrumentCalibration calib = new InstrumentCalibration();
            Start(calib);

        }
        /// <summary>
        /// Начало программы, вызоз калибровки или выход из приложения
        /// </summary>
        /// <param name="calib"></param>
        static void Start(InstrumentCalibration calib)
        {
            Console.WriteLine("Нажмите Enter для начала или любую другую клавишу для выхода");
            if (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                calib.StartColibration(10);
                Console.WriteLine("Желаете повторить калибровку?");
                Start(calib);
            }
            else
            {
                Console.WriteLine("\nДо свидания!");
                Console.ReadKey();
            }
            
        }

    }
}
