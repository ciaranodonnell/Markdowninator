
using System;

namespace Markdowninator.Interface;

public interface IMarkdowninator
{

    string GetFunctionFromClass(string className, string functionName, bool justContents = false);


}