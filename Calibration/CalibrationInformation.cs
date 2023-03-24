using System;

namespace Calibration
{
    internal class CalibrationInformation
    {
        private Device _device;

        private string _fullName;

        private int _numberColibration;

        private double[] _arrayK;

        //private double _k;

        public Device Device { get { return _device; } }
        public string FullName { get { return _fullName; } }
        public int NumberColibration { get { return _numberColibration; } set { _numberColibration = value; } }
        public double[] ArrayK { get { return _arrayK; } set { _arrayK = value; } }
        

        public CalibrationInformation(Device Device)
        {
            _device = Device;
            _fullName = Device.Name + Device.SerialNumber;
            _numberColibration = 0;
            _arrayK = Array.Empty<double>();
        }

        public double CountK((double E, double M) pair)
        {
            switch (Device.Name)
            {
                case "A":
                    return pair.E - pair.M + 1;
                case "B":
                    return pair.E - pair.M + 2;
                case "C":
                    return pair.E - pair.M + 0.5;
                default:
                    return pair.E - pair.M;
            }
        }
    }
}
