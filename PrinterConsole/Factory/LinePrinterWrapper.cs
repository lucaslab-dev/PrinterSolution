using System.Diagnostics;

namespace PrinterConsole.Factory;

public class LinePrinterWrapper(string printerName) : IPrinter
{
	public Task PrintAsync(byte[] data)
	{
		var isRunningOnWindows = Environment.OSVersion.Platform == PlatformID.Win32NT;
		var startInfo = new ProcessStartInfo
		{
			FileName = isRunningOnWindows ? "print" : "lp",
			Arguments = isRunningOnWindows ? $"/D:{printerName}" : $"-d {printerName}",
			RedirectStandardInput = true,
			RedirectStandardOutput = true,
			UseShellExecute = false
		};

		using var process = Process.Start(startInfo);
		if (process == null)
		{
			Console.WriteLine("Error starting the print process.");
			return Task.CompletedTask;
		}

		process.StandardInput.BaseStream.Write(data, 0, data.Length);
		process.StandardInput.Close();

		process.WaitForExit();

		if (process.ExitCode == 0)
		{
			Console.WriteLine("Printing successful!");
		}
		else
		{
			Console.WriteLine($"Error printing: {process.ExitCode}");
		}

		return Task.CompletedTask;
	}
}
