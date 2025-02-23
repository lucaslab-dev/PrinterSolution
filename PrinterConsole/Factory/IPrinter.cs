namespace PrinterConsole.Factory;

public interface IPrinter
{
	Task PrintAsync(byte[] data);
}
