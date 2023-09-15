using Microsoft.Extensions.Hosting;

// sets up and runs an Azure Functions worker host using the HostBuilder 
// and Azure Functions-specific configurations provided by ConfigureFunctionsWorkerDefaults(). 
// It's the entry point for your Azure Functions application, where the host is started, and your functions are executed in response to events.

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .Build();

host.Run();
