using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace walkers.assessment
{
    public class ResponseMessage
    {
        public string currenttimestamp { get; set; }
        public int number { get; set; }
        public string name { get; set; }
    }

    public static class walker_assessment_function
    {
        [FunctionName("walker_function")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req, ILogger log)
        
        {
            string number_str = req.Query["number"];
            int number;
            string name = string.Empty;
            var res = int.TryParse(number_str, out number);

            // first we need to test if we got a valid number. It has to be a valid number and be between 1 and 20
            if (!res || number < 1 || number > 20)
                return new JsonResult("{'error': 'You should provide a valid number between 1 and 20'}");

            // Checking if the number can be divided by 3
            if (number % 3 == 0)
                name = "walkers ";

            // Checking if the number can be divided by 5
            if (number % 5 == 0)
                name = name + "assessment";

            //Please note that the number cam be both divided by 3 and 5. As we see in the if above,
            // there is a concatenation that will happen when the number can be divided by noth numbers
            var response_message = new ResponseMessage() {
                name = name.Trim(),
                number = number,
                currenttimestamp = DateTime.Now.ToString("yyyyMMddHHmmssffff")
            };

            var jsonToReturn = JsonConvert.SerializeObject(response_message);
            return new JsonResult(jsonToReturn);
        }
    }
}
