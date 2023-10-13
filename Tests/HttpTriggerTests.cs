using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Company.Function;
using Moq;
using System.Net;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.Common;
using System.Security.Claims;
using Microsoft.Azure.Functions.Worker;
using static Tests.HttpTriggerTests;

namespace Tests;

[TestClass]
public class HttpTriggerTests
{
    [TestMethod]
    public void Run_WithNameInQueryString_ShouldReturnExpectedResponse()
    {
        // Arrange
        var loggerFactory = new Mock<ILoggerFactory>();
        var logger = new Mock<ILogger>().Object; // Create a mock ILogger
        loggerFactory.Setup(lf => lf.CreateLogger(It.IsAny<string>())).Returns(logger);

        var myTestFunctionContext = new Mock<MyTestFunctionContext>();
        var request = new MyTestHttpRequest(myTestFunctionContext.Object)
        {
            HttpStatusCode = HttpStatusCode.OK
        };

        var function = new HttpTrigger(loggerFactory.Object);

        // Act
        var response = function.Run(req: request, "Quoc");

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        // reset the position to the beginning of the stream
        response.Body.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(response.Body);
        var responseBody = reader.ReadToEnd();
        Assert.AreEqual("Welcome to Azure Functions, Quoc!", responseBody);
    }

    [TestMethod]
    public void Run_NoNameInQueryString_ShouldReturnExpectedResponse()
    {
        // Arrange
        var loggerFactory = new Mock<ILoggerFactory>();
        var logger = new Mock<ILogger>().Object; // Create a mock ILogger
        loggerFactory.Setup(lf => lf.CreateLogger(It.IsAny<string>())).Returns(logger);

        var myTestFunctionContext = new Mock<MyTestFunctionContext>();
        var request = new MyTestHttpRequest(myTestFunctionContext.Object)
        {
            HttpStatusCode = HttpStatusCode.OK
        };

        var function = new HttpTrigger(loggerFactory.Object);

        // Act
        var response = function.Run(req: request, string.Empty);

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        // reset the position to the beginning of the stream
        response.Body.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(response.Body);
        var responseBody = reader.ReadToEnd();
        Assert.AreEqual("Welcome to Azure Functions!", responseBody);
    }

    public class MyTestHttpRequest : HttpRequestData
    {
        public MyTestHttpRequest(FunctionContext functionContext) : base(functionContext)
        {
        }

        public override Stream Body => throw new NotImplementedException();

        public override HttpHeadersCollection Headers => throw new NotImplementedException();

        public override IReadOnlyCollection<IHttpCookie> Cookies => throw new NotImplementedException();

        public override Uri Url => throw new NotImplementedException();

        public override IEnumerable<ClaimsIdentity> Identities => throw new NotImplementedException();

        public override string Method => throw new NotImplementedException();

        public override HttpResponseData CreateResponse()
        {
            HttpResponseData httpResponseData = new MyTestHttpResponseData(FunctionContext);

            return httpResponseData;
        }

        // Custom Properties to set test data
        public HttpStatusCode HttpStatusCode { get; set; }
    }

    public class MyTestFunctionContext : FunctionContext
    {
        public MyTestFunctionContext()
        {

        }
        public override string InvocationId => throw new NotImplementedException();

        public override string FunctionId => throw new NotImplementedException();

        public override TraceContext TraceContext => throw new NotImplementedException();

        public override BindingContext BindingContext => throw new NotImplementedException();

        public override RetryContext RetryContext => throw new NotImplementedException();

        public override IServiceProvider InstanceServices { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override FunctionDefinition FunctionDefinition => throw new NotImplementedException();

        public override IDictionary<object, object> Items { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override IInvocationFeatures Features => throw new NotImplementedException();
    }

    public class MyTestHttpResponseData : HttpResponseData
    {
        private Stream _memoryStream;

        public MyTestHttpResponseData(FunctionContext functionContext) : base(functionContext)
        {
            Headers = new HttpHeadersCollection();
            _memoryStream = new MemoryStream(); // create expandable memory stream
        }

        public override HttpStatusCode StatusCode { get; set; }
        public override HttpHeadersCollection Headers { get; set; }
        public override Stream Body { get => _memoryStream; set => _memoryStream = value; }
        public override HttpCookies Cookies => throw new NotImplementedException();
    }
}
