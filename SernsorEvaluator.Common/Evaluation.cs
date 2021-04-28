namespace SensorEvaluator.Common
{
    public class Evaluation
    {
        public string SensorName { get; set; }
        public string Value { get; set; }

        public Evaluation(string sensorName, string value)
        {
            SensorName = sensorName;
            Value = value;
        }

        public override string ToString()
        {
            return $@"""{SensorName}"": ""{Value}""";
        }
    }
}
