using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Common;

namespace Company.Function
{
    public class HttpTriggerReturnsData
    {
        private readonly ILogger _logger;

        public HttpTriggerReturnsData(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<HttpTriggerReturnsData>();
        }

        // [Function("HttpTriggerReturnsData")]
        // public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        // {
        //     _logger.LogInformation("C# HTTP trigger function processed a request.");

        //     var response = req.CreateResponse(HttpStatusCode.OK);
        //     response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

        //     response.WriteString("Welcome to Azure Functions HttpTriggerReturnsData!");

        //     return response;
        // }

        [Function("HttpTriggerReturnsData")]
        public static async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
        FunctionContext context)
        {
            var logger = context.GetLogger<HttpTriggerReturnsData>();
            logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            await response.WriteStringAsync("Welcome to Azure Functions HttpTriggerReturnsData asynchronously !");

            return response;
        }
    }
}
