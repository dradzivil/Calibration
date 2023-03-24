using System;
using System.Collections.Generic;

namespace Calibration
{
    internal class InstrumentCalibration
    {
        Random rand = new Random();
        // строки для вывода
        string str1 = "Для M = {0:0.000} и Е = {1:0.000} относительная погрешность = {2:0.000}%";
        string str2 = "Для Е = {0:0.000} и M = {1:0.000} поправочное значение К = {2:0.000}";
        
        
        private double[] Array((double M, double E)[] arrayME, Func<(double M, double E), double> myMethodCount)
        {
            double[] array = new double[arrayME.Length];
            for (int i = 0; i < arrayME.Length; i++)
            {
                array[i] = myMethodCount(arrayME[i]);
            }
            return array;
        }
        private double CountError((double M, double E) ME) => Math.Abs(ME.M - ME.E) / ME.E * 100;
      

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
                        dev.Device.Information();
                        return dev;
                    }
                }
            }
            Console.WriteLine("Выберите прибор из списка!");
            return TypeOfDevice(calibDev);
        }

        private void Print((double M, double E)[] arrayME, double[] array, string str)
        {
            for (int i = 0; i < arrayME.Length; i++)
            {
                Console.WriteLine(String.Format(str, arrayME[i].M, arrayME[i].E, array[i]));
            }
            Console.WriteLine("Где M - случайно измеренное значение, Е - эталонное значение\n");
        }

        public void StartColibration(HashSet<Device> dev, CalibrationInformation[] calibDev, (double M, double E)[] arrayME)
        {
            
            double[] Error = Array(arrayME, CountError);
            
            Print(arrayME, Error, str1);

            CalibrationInformation choosenDevice = TypeOfDevice(calibDev);

            choosenDevice.ArrayK = Array(arrayME, choosenDevice.CountK);
            
            Print(arrayME, choosenDevice.ArrayK, str2);
            
            for (int i = 0; i< arrayME.Length; i++)
            {
                Console.WriteLine(
                String.Format(
                    "M = {0:0.000} (с учетом поправочного К = {1:0.000}) " +
                    "и Е = {2:0.000} с относительной погрешностью {3:0.000}%", 
                    arrayME[i].M + choosenDevice.ArrayK[i], choosenDevice.ArrayK[i], arrayME[i].E, Error[i]));
            }
            Console.WriteLine("Где M - случайно измеренное значение, Е - эталонное значение\n");

            choosenDevice.NumberColibration++;
            Console.WriteLine("Всего у прибора " + choosenDevice.FullName + " было произведено калибровок: " 
                + choosenDevice.NumberColibration);
        }
    }
}