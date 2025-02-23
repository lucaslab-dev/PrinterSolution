using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((_, config) =>
    {
      config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
      config.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
      config.AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
      var rabbitMqConfig = context.Configuration.GetSection("RabbitMQ");
      var restaurantSettings = context.Configuration.GetSection("RestaurantSettings");

      services.AddMassTransit(x =>
      {
        x.AddConsumer<PrintJobConsumer>();

        x.UsingRabbitMq((context, cfg) =>
          {
            cfg.Host(rabbitMqConfig["Host"], rabbitMqConfig["VirtualHost"], h =>
              {
                h.Username(rabbitMqConfig["Username"]);
                h.Password(rabbitMqConfig["Password"]);
              });

            var restaurantIdentifier = restaurantSettings["Identifier"];
            cfg.ReceiveEndpoint($"print-job-queue-{restaurantIdentifier}", e =>
            {
              e.Bind("PrintJobMessage.Direct", x =>  // Changed from PrintJobMessage.Topic to match the API
              {
                x.RoutingKey = restaurantIdentifier;
                x.ExchangeType = RabbitMQ.Client.ExchangeType.Direct;
              });
              e.ConfigureConsumer<PrintJobConsumer>(context);
            });
          });
      });
    })
    .Build();

await host.RunAsync();

public class PrintJobConsumer : IConsumer<PrintJobMessage>
{
  public Task Consume(ConsumeContext<PrintJobMessage> context)
  {
    var message = context.Message;

    // Simulate sending to a local printer
    Console.WriteLine($"{DateTime.UtcNow}");
    Console.WriteLine($"Restaurant Identifier {message.Identifier}: Printing Job: {message.JobId}");
    Console.WriteLine($"Document: {message.DocumentName}");
    Console.WriteLine($"Content: {message.Content}");
    Console.WriteLine("----------------------------------------------");

    // Add actual printer logic here (e.g., using System.Drawing.Printing)

    return Task.CompletedTask;
  }
}