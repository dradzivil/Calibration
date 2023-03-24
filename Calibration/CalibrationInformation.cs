using System;

namespace Calibration
{
    /// <summary>
    /// Расширение для приборов, доп информация, касающаяся калибрации
    /// </summary>
    internal class CalibrationInformation
    {
        private Device _device;

        /// <summary>
        /// Тип прибора + серийный номер в одной переменной для удобства
        /// </summary>
        private string _fullName;

        /// <summary>
        /// Количество калибровок конкретного прибора
        /// </summary>
        private int _numberColibration;

        /// <summary>
        /// Массив для хранения поправочных коэффицентов (хранится только последний рассчет)
        /// </summary>
        private double[] _arrayK;

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

        /// <summary>
        /// Подсчет поправочного коэффицента взависимости от типа прибора
        /// </summary>
        /// <param name="pair"></param>
        /// <returns></returns>
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
