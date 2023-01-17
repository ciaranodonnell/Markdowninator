
using MDDG.Interface;
using System;

namespace MDDG.Core;

public class Markdowninator : IMarkdowninator
{
    private readonly FunctionExtractor extractor;
    private readonly string projectRootDirectory;
    private readonly IFileFinder fileFinder;

    public Markdowninator(string projectRootDirectory, IFileFinder fileFinder)
    {
        this.extractor = new FunctionExtractor(fileFinder);
        this.projectRootDirectory = projectRootDirectory;
        this.fileFinder = fileFinder;
    }

    public string GetFunctionFromClass(string fileName, string className, string functionName, bool justContents = false)
    {
        var bodies = extractor.GetFunctionBody(projectRootDirectory, fileName, className, functionName);

        if (bodies.Length == 0)
        {
            return "<FUNCTION NOT FOUND>";
        }
        else if (bodies.Length == 1)
        {
            return bodies[0];
        }
        else
        {
            return "<AMBIGUOUS FUNCTION SPECIFIED>";
        }
    }

}