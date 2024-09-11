using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Drawing.Printing;
namespace EasyFileOps;

public class PDFWriter : IDisposable
{
    private FileStream _stream;
    private PrintDocument _printDocument;

    public PDFWriter(FileStream stream)
    {
        _stream = stream;
    }

    public OperationResult Write(PrintDocument printDocument)
    {
        try
        {
            _printDocument = printDocument;
        
            _printDocument.PrinterSettings.PrintToFile = true;
          
            _printDocument.PrinterSettings.PrintFileName = _stream.Name;
            _printDocument.Print();
            _printDocument.PrintController = new StandardPrintController();
            return new OperationResult
            {
                IsSuccess = true,
                Message = "success print pdf"
            };
        }
        catch (Exception ex) {
            Console.Error.WriteLine(ex.Message);
            return new OperationResult() { IsSuccess = false,Message = ex.Message};
        }
    }
    public void Dispose()
    {
        _stream?.Dispose();
    }
 
}