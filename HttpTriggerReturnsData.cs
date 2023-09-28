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
            Task<string> reverseStringAsync = LibraryFunctions.ReverseStringAsync("Quoc");

            logger.LogInformation("Continue procedural code.");

            // suspend code, return to call, wait to for completion before continuation
            var result = await reverseStringAsync;

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            // the await keyword allows the code to pause and wait for the asynchronous operation to 
            // complete and returns to the caller, but it doesn't block the thread, enabling other tasks to be processed concurrently.
            await response.WriteStringAsync($"Welcome to Azure Functions HttpTriggerReturnsData asynchronously. ReverseStringAsync result: {result}!");

            return response;
        }
    }
}
