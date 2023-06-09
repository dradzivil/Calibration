﻿using System;

namespace Calibration
{
    /// <summary>
    /// Класс, описывающий основную информацию о приборах
    /// </summary>
    internal class Device
    {
        /// <summary>
        /// Название прибора
        /// </summary>
        private string _name;

        /// <summary>
        /// Серийный номер
        /// </summary>
        private int _serialNumber;

        public string Name { get { return _name; } }
        public int SerialNumber { get { return _serialNumber; } }
        public Device(string Name, int SerialNumber) 
        {
            _name = Name;
            _serialNumber = SerialNumber;
        }

        /// <summary>
        /// Строка с краткой инфомацией о приборе
        /// </summary>
        /// <returns></returns>
        public string Information() 
            => String.Format("Информация о приборе типа {0} от {1:yyyy.MM.dd hh.mm.ss} с порядковым номером {2}", 
                Name, DateTime.Now, SerialNumber);
    }
}
