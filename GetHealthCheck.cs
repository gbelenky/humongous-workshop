using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Humongous.Health
{
    public static class GetHealthCheck
    {
        [FunctionName("GetHealthCheck")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string submissionId = req.Query["submissionId"];
            string patientId = req.Query["patientId"];

            if ((string.IsNullOrEmpty(patientId) && string.IsNullOrEmpty(submissionId)) || (!string.IsNullOrEmpty(patientId) && !string.IsNullOrEmpty(submissionId)))
            {
                return new BadRequestObjectResult("Please provide either a patientId or a submissionId");
            }

            
            HealthCheck healthCheck = new HealthCheck();
            healthCheck.SubmissionId = submissionId;
            healthCheck.PatientId = patientId;
            healthCheck.SubmissionDateTime = DateTime.Now;
            healthCheck.HealthStatus = "Good";
            healthCheck.Symptoms[0] = "well";
            healthCheck.Symptoms[1] = "unwell";
            healthCheck.Symptoms[2] = "coughing";
            healthCheck.Symptoms[3] = "sneezing";


            string responseMessage = String.Empty;
            if (!string.IsNullOrEmpty(submissionId))
            {
                return new OkObjectResult(healthCheck);


            }

            if (!string.IsNullOrEmpty(patientId))
            {
                return new OkObjectResult(healthCheck);
            }

            return new BadRequestObjectResult("will never reach that point");
        }
    }
}
