namespace PrinterConsole.Factory;

public class SerialPrinterWrapper(string portName, int baudRate) : IPrinter
{
	private readonly SerialPrinter _printer = new(portName: portName, baudRate: baudRate);

	public Task Print(byte[] data) => Task.Run(() => _printer.Write(data));
}
