using System.Net;
using Microsoft.Azure.Functions.Worker.Http;
using static Tests.HttpTriggerTests;

namespace Tests.Helpers
{
    public static class TestExtensions
    {
        public static HttpResponseData CreateResponse(this MyTestHttpRequest request, HttpStatusCode statusCode)
        {
            HttpResponseData httpResponseData = request.CreateResponse();
            httpResponseData.StatusCode = statusCode;
            return httpResponseData;
        }
    }
}
