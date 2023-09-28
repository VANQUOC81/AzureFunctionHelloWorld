using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Common;

namespace Company.Function
{
    public class HttpTriggerReturnsData
    {
        [Function("HttpTriggerReturnsData")]
        public static async Task<HttpResponseData> RunAsync(
       [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
       FunctionContext context)
        {
            var logger = context.GetLogger<HttpTriggerReturnsData>();
            logger.LogInformation("C# HTTP trigger function processed a request.");

            // calling library function
            var result = await LibraryFunctions.ReverseStringAsync("Quoc");
                        
            logger.LogInformation("await done ReverseStringAsync completed it's operation.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            // the await keyword allows the code to pause and wait for the asynchronous operation to complete, but it doesn't block the thread, enabling other tasks to be processed concurrently.
            await response.WriteStringAsync($"Welcome to Azure Functions HttpTriggerReturnsData asynchronously with a result of {result}!");

            return response;
        }
    }
}
