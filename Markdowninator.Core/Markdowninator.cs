
using Markdowninator.Interface;
using System;

namespace Markdowninator.Core;

public class Markdowninator : IMarkdowninator
{

    public string GetFunctionFromClass(string className, string functionName, bool justContents = false)
    {
        return "didCallMethod";
    }

}