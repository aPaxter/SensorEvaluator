using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SensorEvaluator.Common
{

    public abstract class Sensor
    {
        public string Name { get; set; }
        public List<SensorReading> Readings { get; set; } = new List<SensorReading>();
        public SensorType Type { get; set; }

        protected Sensor(string name, SensorType type)
        {
            Name = name;
            Type = type;
        }

        public abstract Evaluation GetEvaluation(double correctValue);
    }

    public class Termometer : Sensor
    {
        public Termometer(string name, SensorType type) : base(name, type) { }
        public override Evaluation GetEvaluation(double correctValue)
        {
            var deviation = Math.Abs(Readings.Average(x => x.Value) - correctValue);
            string result;

            if (deviation > 3)
            {
                result = "standard";
            }
            else if (deviation > 0.5)
            {
                result = "very precise";
            }
            else
            {
                result = "ultra precise";
            }

            return new Evaluation(Name, result);
        }
    }

    public class Humidity : Sensor
    {
        public Humidity(string name, SensorType type) : base(name, type) { }
        public override Evaluation GetEvaluation(double correctValue)
        {
            var deviation = Math.Abs(Readings.Average(x => x.Value) - correctValue);
            var allowedDeviation = correctValue / 100;
            var result = deviation > allowedDeviation ? "discard" : "keep";

            return new Evaluation(Name, result);
        }
    }

    public class Monooxide : Sensor
    {
        public Monooxide(string name, SensorType type) : base(name, type) { }
        public override Evaluation GetEvaluation(double correctValue)
        {
            var result = Readings.Any(x => Math.Abs(correctValue - x.Value) > 3) ? "discard" : "keep";

            return new Evaluation(Name, result);
        }
    }

    public class SensorReading
    {
        public DateTime RegistrationDateTime { get; set; }
        public double Value { get; set; }
    }
}
