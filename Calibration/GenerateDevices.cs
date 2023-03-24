using System;
using System.Collections.Generic;

namespace Calibration
{
    internal class GenerateDevices
    {
        Random rand = new Random();
        static string[] names = new[] { "A", "B", "C" };
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
    }
}
