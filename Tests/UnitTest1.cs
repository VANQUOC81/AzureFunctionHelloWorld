using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Company.Function;
using Moq;
using System.Net;
using System.Text;

namespace Tests;

[TestClass]
public class HttpTriggerTests
{
    [TestMethod]
    public void Run_ValidName_ShouldReturnExpectedResponse()
    {
        // Arrange
        var loggerFactory = new Mock<ILoggerFactory>();
        var logger = new Mock<ILogger>().Object; // Create a mock ILogger
        loggerFactory.Setup(lf => lf.CreateLogger(It.IsAny<string>())).Returns(logger);
        
        var request = new Mock<HttpRequestData>();
        var httpResponse = new Mock<HttpResponseData>();

        // Configure the mock HttpResponseData to return the desired status code and body
        httpResponse.Setup(r => r.StatusCode).Returns(HttpStatusCode.OK);
        httpResponse.Setup(r => r.Body).Returns(new MemoryStream(Encoding.UTF8.GetBytes("Welcome to Azure Functions, John!")));
        httpResponse.Setup(r => r.Headers).Returns(new HttpHeadersCollection());

        // Set up the HttpRequestData mock to create the configured response
       // request.Setup(r => r.CreateResponse(HttpStatusCode.OK)).Returns(httpResponse.Object);

        var name = "John"; // Replace with your test data
        var function = new HttpTrigger(loggerFactory.Object);

        // Act
        var response = function.Run(request.Object, name);

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)response.StatusCode);
        Assert.IsTrue(response.Body.ToString() == "Welcome to Azure Functions, John!");
    }

    // [TestMethod]
    // public void Run_NoName_ShouldReturnDefaultResponse()
    // {
    //     // Arrange
    //     var logger = new LoggerFactory().CreateLogger("Test");
    //     var request = new Mock<HttpRequestData>();
    //     var httpResponse = new Mock<HttpResponseData>();

    //     // Configure the mock HttpResponseData to return the desired status code and body
    //     httpResponse.Setup(r => r.StatusCode).Returns(HttpStatusCode.OK);
    //     httpResponse.Setup(r => r.Body).Returns(new MemoryStream(Encoding.UTF8.GetBytes("Welcome to Azure Functions!")));
    //     httpResponse.Setup(r => r.Headers).Returns(new HttpHeadersCollection());

    //     // Set up the HttpRequestData mock to create the configured response
    //     request.Setup(r => r.CreateResponse(HttpStatusCode.OK)).Returns(httpResponse.Object);

    //     var function = new HttpTrigger(logger);

    //     // Act
    //     var response = function.Run(request.Object, null);

    //     // Assert
    //     Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)response.StatusCode);
    //     Assert.IsTrue(response.Body.AsString() == "Welcome to Azure Functions!");
    // }
}




