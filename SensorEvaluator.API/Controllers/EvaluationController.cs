using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SensorEvaluator.Common;

namespace SensorEvaluator.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EvaluationController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            var fileText = System.IO.File.ReadAllText(@"c:\temp\sensor data.txt");

            var result = Evaluator.EvaluateLogFile(fileText);

            return result;
        }
    }
}
