using PrinterConsole.Settings;

namespace PrinterConsole.Factory;

public static class PrinterFactory
{
    public static IPrinter Create(PrinterSettings settings)
    {
        return settings.PrinterType switch
        {
            PrinterType.Samba => new SambaPrinterWrapper(
                settings.TempPath ?? "/tmp",
                settings.FilePath ?? "//computer/printer"),

            PrinterType.ImmediateNetwork => new NetworkPrinterWrapper(
                settings.HostnameOrIp ?? "localhost",
                settings.Port ?? 9100,
                settings.PrinterName ?? "DefaultPrinter"),

            PrinterType.Serial => new SerialPrinterWrapper(
                settings.PortName ?? "/dev/tty.usbserial",
                settings.BaudRate ?? 115200),

            PrinterType.File => new FilePrinterWrapper(
                settings.FilePath ?? "/dev/usb/lp0"),

            PrinterType.LinePrinter => new LinePrinterWrapper(
                                settings.PrinterName ?? "DefaultPrinter"),

            _ => throw new ArgumentException("Invalid printer type", nameof(settings.PrinterType))
        };
    }
}
