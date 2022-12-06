using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Humongous.Health
{
    public static class GetHealthCheckByPatient
    {
        [FunctionName("GetHealthCheckByPatient")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetHealthCheckByPatient/{patientId}")] HttpRequest req,
            [CosmosDB(
                databaseName: "HealthCheck",
                collectionName: "healthchecks",
                ConnectionStringSetting = "CosmosDBConnection",
                SqlQuery ="SELECT * FROM c WHERE c.PatientId={patientId} ORDER BY c._ts DESC")] IEnumerable<HealthCheck> healthChecks,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");


            if (Utils.Count(healthChecks) == 0)
            {
                return new NotFoundResult();
            }
            IEnumerator<HealthCheck> i = healthChecks.GetEnumerator();
            i.MoveNext();
            HealthCheck retValue = i.Current;
            return new OkObjectResult(retValue);
        }
    }
}
