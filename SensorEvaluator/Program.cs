using System;
using System.IO;
using System.Text.RegularExpressions;
using SensorEvaluator.Common;

namespace SensorEvaluator
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileText = File.ReadAllText(@"c:\temp\sensor data.txt");
            var result = Evaluator.EvaluateLogFile(fileText);

            Console.Write(result);
        }
    }
}
