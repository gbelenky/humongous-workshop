
using System;

namespace Humongous.Health
{
 public class HealthCheck
    {
        public HealthCheck() { 
            Symptoms =  new string[14];
        }
        // generate this
        public string SubmissionId
        {
            get; set;
        }

        public string PatientId
        {
            get; set;
        }

        public DateTime SubmissionDateTime
        {
            get; set;
        }
        public string HealthStatus
        {
            get; set;
        }

        public string[] Symptoms
        {
            get; set;
        }
        
    }
}