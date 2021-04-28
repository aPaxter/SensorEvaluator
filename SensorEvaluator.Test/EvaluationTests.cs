using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SensorEvaluator.Common;

namespace SensorEvaluator.Test
{
    [TestClass]
    public class EvaluationTests
    {
        [TestMethod]
        public void GetEvaluationForTermometer_HighMistake_ShouldBePrecise()
        {
            var sensorData = new SensorData()
            {
                Temperature = 20.0,
                Sensors = new List<Sensor>()
                {
                    new Termometer("test", SensorType.Termometer)
                    {
                        Readings = new List<SensorReading>()
                        {
                            new SensorReading(){RegistrationDateTime = DateTime.Now, Value = 20},
                            new SensorReading(){RegistrationDateTime = DateTime.Now, Value = 21},
                            new SensorReading(){RegistrationDateTime = DateTime.Now, Value = 22},
                        }
                    }
                }
            };

            var sensor = sensorData.Sensors.Single();
            var evaluation = sensor.GetEvaluation(sensorData.GetSensorCorrectValue(sensor.Type));

            evaluation.Value.Should().Be("precise");
        }

        [TestMethod]
        public void GetEvaluationForTermometer_LowMistakeAndHighDeviation_ShouldBeVeryPrecise()
        {
            var sensorData = new SensorData()
            {
                Temperature = 20.0,
                Sensors = new List<Sensor>()
                {
                    new Termometer("test", SensorType.Termometer)
                    {
                        Readings = new List<SensorReading>()
                        {
                            new SensorReading(){RegistrationDateTime = DateTime.Now, Value = 15},
                            new SensorReading(){RegistrationDateTime = DateTime.Now, Value = 20},
                            new SensorReading(){RegistrationDateTime = DateTime.Now, Value = 25},
                        }
                    }
                }
            };

            var sensor = sensorData.Sensors.Single();
            var evaluation = sensor.GetEvaluation(sensorData.GetSensorCorrectValue(sensor.Type));

            evaluation.Value.Should().Be("very precise");
        }

        [TestMethod]
        public void GetEvaluationForTermometer_LowMistakeAndLowDeviation_ShouldBeVeryPrecise()
        {
            var sensorData = new SensorData()
            {
                Temperature = 20.0,
                Sensors = new List<Sensor>()
                {
                    new Termometer("test", SensorType.Termometer)
                    {
                        Readings = new List<SensorReading>()
                        {
                            new SensorReading(){RegistrationDateTime = DateTime.Now, Value = 19},
                            new SensorReading(){RegistrationDateTime = DateTime.Now, Value = 20},
                            new SensorReading(){RegistrationDateTime = DateTime.Now, Value = 21},
                        }
                    }
                }
            };

            var sensor = sensorData.Sensors.Single();
            var evaluation = sensor.GetEvaluation(sensorData.GetSensorCorrectValue(sensor.Type));

            evaluation.Value.Should().Be("ultra precise");
        }
    }
}
