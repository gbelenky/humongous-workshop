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
    public static class CreateHeathCheck
    {
        [FunctionName("CreateHeathCheck")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            [CosmosDB(
                databaseName: "HealthCheck",
                collectionName: "healthchecks",
                ConnectionStringSetting = "CosmosDBConnection")] IAsyncCollector<HealthCheck> documentsOut,
            ILogger log)
        {

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            HealthCheck healthCheck = JsonConvert.DeserializeObject<HealthCheck>(requestBody);
            healthCheck.SubmissionId = Guid.NewGuid().ToString();
            healthCheck.SubmissionDateTime = DateTime.Now;
            await documentsOut.AddAsync(healthCheck);
            
            string responseMessage = $"Submission for patientId: {healthCheck.PatientId} was added successfully";
               
            return new OkObjectResult(responseMessage);
        }
    }
}
