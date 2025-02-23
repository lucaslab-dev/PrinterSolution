using MassTransit;
using Microsoft.Extensions.Options;

using Shared;
using PrinterConsole.Factory;
using PrinterConsole.Settings;

public sealed class PrintJobConsumer(IOptions<PrinterSettings> printerSettings) : IConsumer<PrintJobMessage>
{
  public async Task Consume(ConsumeContext<PrintJobMessage> context)
  {
    var message = context.Message;

    Console.WriteLine($"{DateTime.UtcNow}");
    Console.WriteLine($"Store Identifier {message.Identifier}: Printing Job: {message.JobId}");
    Console.WriteLine($"Document: {message.DocumentName}");
    Console.WriteLine($"Content: {message.Content}");
    Console.WriteLine("----------------------------------------------");

    var printer = PrinterFactory.Create(printerSettings.Value);
    await printer.Print(Convert.FromBase64String(message.Content));
  }
}