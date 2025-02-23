namespace PrinterConsole.Factory;

public class FilePrinterWrapper(string filePath) : IPrinter
{
	private readonly FilePrinter _printer = new(filePath: filePath);

	public Task PrintAsync(byte[] data) => Task.Run(() => _printer.Write(data));
}
