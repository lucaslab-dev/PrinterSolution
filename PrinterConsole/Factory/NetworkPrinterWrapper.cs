namespace PrinterConsole.Factory;

/// <summary>
/// Ethernet or WiFi (This uses an Immediate Printer, no live paper status events, but is easier to use)
/// </summary>
/// <param name="hostnameOrIp"></param>
/// <param name="port"></param>
/// <param name="printerName"></param>
public class NetworkPrinterWrapper(string hostnameOrIp, int port, string printerName) : IPrinter
{
	private readonly ImmediateNetworkPrinter _printer = new(
		new ImmediateNetworkPrinterSettings
		{
			ConnectionString = $"{hostnameOrIp}:{port}",
			PrinterName = printerName
		});

	public Task PrintAsync(byte[] data) => _printer.WriteAsync(data);
}
