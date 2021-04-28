using System;
using System.Collections.Generic;
using System.Linq;

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
            var meanValue = Readings.Average(x => x.Value);
            var meanMistake = Math.Abs(meanValue - correctValue);

            var standardDeviation = Math.Sqrt(Readings.Sum(x => Math.Pow(meanValue - x.Value, 2)) / (Readings.Count - 1));
            string result;

            if (meanMistake > 0.5)
            {
                result = "precise";
            }
            else if (standardDeviation > 3)
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

    public class HumiditySensor : Sensor
    {
        public HumiditySensor(string name, SensorType type) : base(name, type) { }
        public override Evaluation GetEvaluation(double correctValue)
        {
            var allowedDeviation = correctValue / 100;
            var result = Readings.Any(x => Math.Abs(x.Value - correctValue) > allowedDeviation) ? "discard" : "keep";

            return new Evaluation(Name, result);
        }
    }

    public class MonooxideSensor : Sensor
    {
        public MonooxideSensor(string name, SensorType type) : base(name, type) { }
        public override Evaluation GetEvaluation(double correctValue)
        {
            var result = Readings.Any(x => Math.Abs(x.Value - correctValue) > 3) ? "discard" : "keep";

            return new Evaluation(Name, result);
        }
    }

    public class SensorReading
    {
        public DateTime RegistrationDateTime { get; set; }
        public double Value { get; set; }
    }
}
