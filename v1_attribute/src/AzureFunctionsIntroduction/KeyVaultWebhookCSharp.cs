using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace AzureFunctionsIntroduction
{
    public static class KeyVaultWebhookCSharp
    {
        private static HttpClient client = new HttpClient();

        [FunctionName("KeyVaultWebhookCSharp")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");
            // Make sure AppService can access to KeyVault by
            // 1. Enable Managed Service Identity : AppService
            // 2. Enable AccessPolicy for AppService. : KeyVault
            // 3. Set Secret at KeyVault.
            // 4. Set KeyVault Secret Uri as AzureFunctions EnvironmentVariables, the key is "KeyVaultSecretUri".
            // 5. Deploy App and try access to the FunctionUri!
            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            var kvClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback), client);
            var secret = (await kvClient.GetSecretAsync(Environment.GetEnvironmentVariable("KeyVaultSecretUri"))).Value;
            return req.CreateResponse(HttpStatusCode.OK, new
            {
                Value = secret,
            });
        }
    }
}
