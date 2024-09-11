using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyFileOps;

public interface IFileHelper
{
    public OperationResult CreateFile(string path);
    public OperationResult DeleteFile(string path);
    public OperationResult MoveFile(string source,string destination);
    public OperationResult RenameFile(string AbsoultePath,string oldName,string newName);
    public OperationResult WriteLine(string path, StringBuilder content,bool IsAppend);
    public Task<OperationResult> WriteLineAsync(string path, StringBuilder content, bool IsAppend);
    public Task<OperationResult> ConvertTxtToPDF(string PDfpath,string TxtPath,string fontFamily,float fontSize);
    public Task<OperationResult> ConvertTxtToHTML(string path);
}
