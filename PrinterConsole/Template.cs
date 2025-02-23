using ESCPOS_NET.Emitters;
using ESCPOS_NET.Utilities;

namespace PrinterConsole;

public class Template
{
    public static byte[] Recipe(string content)
    {
        var e = new EPSON();
        return ByteSplicer.Combine(
            e.CenterAlign(),
            e.SetBarcodeHeightInDots(360),
            e.SetBarWidth(BarWidth.Default),
            e.SetBarLabelPosition(BarLabelPrintPosition.None),
            e.PrintBarcode(BarcodeType.ITF, "0123456789"),
            e.PrintLine(""),
            e.PrintLine("CONTENT"),
            e.PrintLine(content),
            e.PrintLine(""),
            e.PrintLine(""),
            e.FeedLines(3),
            e.FullCut(),
            e.Initialize()
        );
    }
}