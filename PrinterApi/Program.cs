using MassTransit;

using Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MessageBrokerSettings>(builder.Configuration.GetSection("RabbitMq"));
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((_, cfg) =>
    {
        var rabbitMqConfig = builder.Configuration.GetSection("RabbitMq").Get<MessageBrokerSettings>()!;
        cfg.Host(rabbitMqConfig.Host, rabbitMqConfig.VirtualHost, h =>
        {
            h.Username(rabbitMqConfig.Username!);
            h.Password(rabbitMqConfig.Password!);
        });

        cfg.Message<PrintJobMessage>(x => x.SetEntityName(MessageBrokerQueues.PrintJobMessage));
        cfg.Publish<PrintJobMessage>(x =>
        {
            x.ExchangeType = RabbitMQ.Client.ExchangeType.Direct;
            x.Durable = true;
            x.AutoDelete = false;
        });
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var lifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();
lifetime.ApplicationStopping.Register(() =>
{
    Console.WriteLine("Application is stopping. Disposing MassTransit bus...");
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/print", async (IPublishEndpoint publishEndpoint, IPublishEndpoint bus) =>
{
    var message = new PrintJobMessage
    {
        JobId = Guid.NewGuid().ToString(),
        DocumentName = "SampleDocument.pdf",
        Content = "This is a sample print job.",
        Identifier = "my-store"
    };

    await publishEndpoint.Publish(message, x => x.SetRoutingKey(message.Identifier));
    return Results.Ok($"{DateTime.UtcNow} Print job published. Job ID: {message.JobId} Identifier: {message.Identifier}");
});

await app.RunAsync();
