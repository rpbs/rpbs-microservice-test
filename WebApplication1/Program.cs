using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Authentication.Negotiate;

var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapGet("/", () =>
{
    return "I'm running";
});

var serviceBusClient = new ServiceBusClient("Endpoint=sb://renyo-service-bus.servicebus.windows.net/;SharedAccessKeyName=full;SharedAccessKey=8EUBmbVrHp3wnP2Xk3yWzZr8hxkvDm50P+ASbK7YRtM=;TransportType=NetMessaging;EntityPath=microservice");

var sender = serviceBusClient.CreateSender("microservice");


app.MapPost("/upload", async (IFormFile file) =>
{
    var tempFile = Path.GetTempFileName();
    app.Logger.LogInformation(tempFile);
    MemoryStream ms = new();
    await file.CopyToAsync(ms);
    BinaryData binaryData = new(ms.ToArray());

    await sender.SendMessageAsync(new ServiceBusMessage(binaryData));

    return ;
});

app.Run();

