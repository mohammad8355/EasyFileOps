using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EasyFileOps;

public class FileHelper : IFileHelper
{
    public async Task<OperationResult> ConvertTxtToHTML(string path)
    {
        try
        {
            var dotIndex = path.IndexOf('.');
            if (dotIndex == -1) return new OperationResult() { IsSuccess = false, Message = "Invalid Path" };
            var modifiedString = path.Substring(0, dotIndex + 1) + "html";
            File.Move(path, modifiedString);
            return new OperationResult()
            {
                IsSuccess = true,
                Message = "SuccessFully Convert text to html"
            };
        }
        catch(Exception ex)
        {
            return new OperationResult()
            {
                IsSuccess = false,
                Message = ex.Message,
            };     
        }

    }

    /// <summary>
    /// convert text file to pdf
    /// </summary>
    /// <param name="path"></param>
    /// <param name="fontFamily">name of font family</param>
    /// <param name="fontSize">font size of text in pdf</param>
    /// <param name="width">width of pdf</param>
    /// <param name="height">height of pdf</param>
    /// <returns>return is success and message </returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<OperationResult> ConvertTxtToPDF(string PDfpath, string TxtPath, string fontFamily, float fontSize)
    {
        try
        {
            string text = File.ReadAllText(TxtPath);
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += (sender, e) =>
            {
                e.Graphics.DrawString(text, new Font(fontFamily,fontSize), Brushes.Black, new RectangleF(0, 0, e.MarginBounds.Width, e.MarginBounds.Height));
            };
            using (var stream = new FileStream(PDfpath, FileMode.Create))
            {
                var writer = new PDFWriter(stream);

                var result = writer.Write(printDocument);
                writer.Dispose();
            return new OperationResult()
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message
            };
            }
        }
        catch (Exception ex) {
            Console.Error.WriteLine(ex.Message);
            return new OperationResult()
            {
                IsSuccess = false,
                Message = ex.Message
            };
        }
    }

    /// <summary>
    /// the method that create file 
    /// </summary>
    /// <param name="path">the path with name of file Example : C:/user/file.txt</param>
    /// <returns>Return IsSuccess And Message</returns>
    public OperationResult CreateFile(string path)
    {
        try
        {
            using (FileStream fs = File.Create(path));
            return new OperationResult()
            {
                IsSuccess = true,
                Message = "Successfully Create File"
            };
        }
        catch (Exception ex) 
        {
            Console.Error.WriteLine(ex.Message);
            return new OperationResult()
            {
                IsSuccess = false,
                Message = ex.Message
            };
        }
        
    }
    /// <summary>
    /// delete specefic file
    /// </summary>
    /// <param name="path">string path with name of file </param>
    /// <returns>return IsSuccess And Message</returns>
    public OperationResult DeleteFile(string path)
    {
        try
        {
            File.Delete(path);
            return new OperationResult()
            {
                IsSuccess = true,
                Message = "File Successfully deleted"
            };
        }
        catch(Exception ex) 
        {
            Console.Error.WriteLine(ex.Message);
            return new OperationResult()
            {
                IsSuccess = false,
                Message = ex.Message
            };
        }
    }
    /// <summary>
    /// move file to another directory 
    /// </summary>
    /// <param name="source">path of source of file</param>
    /// <param name="destination">path of destination of file</param>
    /// <returns>return IsSuccess And Message</returns>
    public OperationResult MoveFile(string source, string destination)
    {
        try
        {
           Directory.Move(source, destination);
            return new OperationResult()
            {
                IsSuccess = true,
                Message = $"Successfully File moved from {source} to {destination}",
            };
        }catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
            return new OperationResult()
            {
                IsSuccess = false,
                Message = ex.Message,
            };
        }
    }
    /// <summary>
    /// Rename file
    /// </summary>
    /// <param name="Absoultepath">the path of file with out name </param>
    /// <param name="oldName">old name of file </param>
    /// <param name="newName">new name of file</param>
    /// <returns></returns>
    public OperationResult RenameFile(string Absoultepath,string oldName, string newName)
    {
        try
        {
            File.Move(Path.Combine(Absoultepath,oldName), Path.Combine(Absoultepath, newName));
            return new OperationResult()
            {
                IsSuccess = true,
                Message = "File Successfully Renamed"
            };
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
            return new OperationResult()
            {
                IsSuccess = false,
                Message = ex.Message
            };
        }
    }
    /// <summary>
    /// write content to the file and option to choose overwrite or append content
    /// </summary>
    /// <param name="path">path of file</param>
    /// <param name="content">content of file</param>
    /// <param name="IsAppend">IsAppend if be true the content add to the existing content otherwise overwrite it</param>
    /// <returns></returns>
    public OperationResult WriteLine(string path, StringBuilder content,bool IsAppend)
    {
        try
        { 
            using (var streamWriter = new StreamWriter(path,IsAppend))
            {
                streamWriter.WriteLine(content);
            }
            return new OperationResult()
            {
                IsSuccess = true,
                Message = "File Successfully Writed",
            };
            
        }
        catch(Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
            return new OperationResult()
            {
                IsSuccess = false,
                Message = ex.Message
            };
        }
    }
    public async Task<OperationResult> WriteLineAsync(string path, StringBuilder content, bool IsAppend = true)
    {
        try
        {
           
            using (var streamWriter = new StreamWriter(path, IsAppend))
            {
                await streamWriter.WriteLineAsync(content);
            }
            return new OperationResult()
            {
                IsSuccess = true,
                Message = "File Successfully Writed",
            };

        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
            return new OperationResult()
            {
                IsSuccess = false,
                Message = ex.Message
            };
        }
    }
}
