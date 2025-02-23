namespace PrinterConsole.Settings;

public class PrinterSettings
{
  public PrinterType PrinterType { get; set; }

  public string? TempPath { get; set; }
  public string? FilePath { get; set; }
  public string? HostnameOrIp { get; set; }
  public int? Port { get; set; }
  public string? PrinterName { get; set; }
  public string? PortName { get; set; }
  public int? BaudRate { get; set; }
}
