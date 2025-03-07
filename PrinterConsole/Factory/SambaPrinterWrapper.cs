namespace PrinterConsole.Factory;

public class SambaPrinterWrapper(string tempPath, string filePath) : IPrinter
{
	private readonly SambaPrinter _printer = new(tempFileBasePath: tempPath, filePath: filePath);

	public Task PrintAsync(byte[] data) => Task.Run(() => _printer.Write(data));
}
