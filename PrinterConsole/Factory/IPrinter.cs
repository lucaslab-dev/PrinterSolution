namespace PrinterConsole.Factory;

public interface IPrinter
{
	Task Print(byte[] data);
}
