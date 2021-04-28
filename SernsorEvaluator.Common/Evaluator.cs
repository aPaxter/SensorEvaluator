using System;

namespace SensorEvaluator.Common
{
    public static class Evaluator
    {
        public static string EvaluateLogFile(string logContentsStr)
        {
            var sensorData = LogParser.Parse(logContentsStr);

            var result = string.Empty;

            foreach (var sensor in sensorData.Sensors)
            {
                if (!string.IsNullOrEmpty(result))
                {
                    result += $",{Environment.NewLine}";
                }
                var correctValue = sensorData.GetSensorCorrectValue(sensor.Type);
                
                var evaluation = sensor.GetEvaluation(correctValue);

                result += $"{evaluation}";

            }

            return result;
        }
    }
}
