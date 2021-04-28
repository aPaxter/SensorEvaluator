using System;
using System.Collections.Generic;

namespace SensorEvaluator.Common
{
    public class SensorData
    {
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double Monooxide { get; set; }
        public List<Sensor> Sensors { get; set; } = new List<Sensor>();

        public double GetSensorCorrectValue(SensorType type)
        {
            switch (type)
            {
                case SensorType.Humidity:
                    return Humidity;
                case SensorType.Termometer:
                    return Temperature;
                case SensorType.Monooxide:
                    return Monooxide;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
