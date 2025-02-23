using MassTransit;
using Microsoft.Extensions.Options;

using PrinterConsole;
using PrinterConsole.Factory;
using PrinterConsole.Settings;

public sealed class PrintJobConsumer : IConsumer<PrintJobMessage>
{
  private readonly PrinterSettings _printerSettings;

  public PrintJobConsumer(IOptions<PrinterSettings> printerSettings)
  {
    _printerSettings = printerSettings.Value;
  }
  
  public async Task Consume(ConsumeContext<PrintJobMessage> context)
  {
    var message = context.Message;

    Console.WriteLine($"{DateTime.UtcNow}");
    Console.WriteLine($"Store Identifier {message.Identifier}: Printing Job: {message.JobId}");
    Console.WriteLine($"Document: {message.DocumentName}");
    Console.WriteLine($"Content: {message.Content}");
    Console.WriteLine("----------------------------------------------");

    var printer = PrinterFactory.Create(_printerSettings);
    
    try
    {
      await printer.PrintAsync(Template.Recipe(message.Content));
    }
    catch (FormatException ex)
    {
      Console.WriteLine($"Error: Invalid Base64 content received - {ex.Message}");
      throw;
    }
  }
}