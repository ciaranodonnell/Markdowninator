
using System;

namespace MDDG.Interface;

public interface IMarkdowninator
{

    string GetFunctionFromClass(string fileName, string className, string functionName, bool justContents = false);


}