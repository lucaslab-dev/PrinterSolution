using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using PrinterConsole.Settings;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((_, config) =>
    {
      config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
      config.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
      config.AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
      services.Configure<MessageBrokerSettings>(context.Configuration.GetSection("RabbitMq"));
      services.Configure<PrinterSettings>(context.Configuration.GetSection("PrinterSettings"));

      var rabbitMqConfig = context.Configuration.GetSection("RabbitMq").Get<MessageBrokerSettings>()!;
      var storeSettings = context.Configuration.GetSection("StoreSettings");

      services.AddMassTransit(x =>
      {
        x.AddConsumer<PrintJobConsumer>();

        x.UsingRabbitMq((context, cfg) =>
          {
            cfg.Host(rabbitMqConfig.Host, rabbitMqConfig.VirtualHost, h =>
            {
              h.Username(rabbitMqConfig.Username!);
              h.Password(rabbitMqConfig.Password!);
            });

            var storeIdentifier = storeSettings["Identifier"];
            cfg.ReceiveEndpoint($"print-job-queue-{storeIdentifier}", e =>
            {
              e.Bind("PrintJobMessage.Direct", x =>
              {
                x.RoutingKey = storeIdentifier;
                x.ExchangeType = RabbitMQ.Client.ExchangeType.Direct;
              });
              e.ConfigureConsumer<PrintJobConsumer>(context);
            });
          });
      });
    })
    .Build();

await host.RunAsync();