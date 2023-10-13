using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public class HttpTrigger
    {
        private readonly ILogger _logger;

        public HttpTrigger(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<HttpTrigger>();
        }

        [Function("HttpTrigger")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req, string name)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            // CreateResponse extension method is being called in unit test
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            // Read the "name" query string variable from the URL
            if (!string.IsNullOrWhiteSpace(name))
            {
                // Concatenate the name with the response
                response.WriteString($"Welcome to Azure Functions, {name}!");
            }
            else
            {
                // If "name" query string variable is not provided, use a default message
                // WriteString extension method is being called in unit test
                response.WriteString("Welcome to Azure Functions!");
            }

            return response;
        }
    }
}
