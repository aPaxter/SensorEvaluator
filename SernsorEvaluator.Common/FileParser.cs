using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SensorEvaluator.Common
{
    public static class FileParser
    {
        public static SensorData Parse(string logContentsStr)
        {
            try
            {
                var fileLines = Regex.Split(logContentsStr, "\r\n|\r|\n");
                var firstLine = fileLines[0].Split(' ');

                var Temperature = double.Parse(firstLine[1]);
                var Humidity = double.Parse(firstLine[2]);
                var Monooxide = double.Parse(firstLine[3]);

                var lineIndex = 1;

                var sensors = new List<Sensor>();

                while (lineIndex < fileLines.Length)
                {
                    var fileLine = fileLines[lineIndex].Split(' ');
                    var sensorType = ParseSensorType(fileLine[0]);
                    var name = fileLine[1];

                    var sensor = CreateSensor(name, sensorType);

                    lineIndex++;

                    while (lineIndex < fileLines.Length && !CheckLineIsNextHeader(fileLines[lineIndex]))
                    {
                        var valueLine = fileLines[lineIndex].Split(' ');
                        var date = DateTime.Parse(valueLine[0]);
                        var value = double.Parse(valueLine[1]);

                        sensor.Readings.Add(new SensorReading()
                        {
                            RegistrationDateTime = date,
                            Value = value
                        });

                        lineIndex++;
                    }

                    sensors.Add(sensor);
                }

                return new SensorData()
                {
                    Temperature = Temperature,
                    Humidity = Humidity,
                    Monooxide = Monooxide,
                    Sensors = sensors
                };
            }
            catch
            {
                Console.WriteLine("Wrong file input");
                throw;
            }
        }

        private static bool CheckLineIsNextHeader(string fileLine)
        {
            var result = Regex.Match(fileLine, @"(humidity|thermometer|monoxide)");

            return result.Success;
        }

        private static Sensor CreateSensor(string name, SensorType sensor)
        {
            switch (sensor)
            {
                case SensorType.Humidity:
                    return new Humidity(name, sensor);
                case SensorType.Termometer:
                    return new Termometer(name, sensor);
                case SensorType.Monooxide:
                    return new Monooxide(name, sensor);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static SensorType ParseSensorType(string value)
        {
            switch (value)
            {
                case "humidity":
                    return SensorType.Humidity;
                case "thermometer":
                    return SensorType.Termometer;
                case "monoxide":
                    return SensorType.Monooxide;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
