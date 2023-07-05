using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Lister02
{
    public class Listener02
    {
        private readonly ILogger _logger;

        public Listener02(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Listener02>();
        }

        [Function("Listener02")]
        public void Run([ServiceBusTrigger("microservice", 
            Connection = "Endpoint=sb://renyo-service-bus.servicebus.windows.net/;SharedAccessKeyName=full;SharedAccessKey=8EUBmbVrHp3wnP2Xk3yWzZr8hxkvDm50P+ASbK7YRtM=;EntityPath=microservice")] string myQueueItem)
        {
            _logger.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
