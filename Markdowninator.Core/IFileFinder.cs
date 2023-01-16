namespace Markdowninator.Core
{
    public interface IFileFinder
    {
        List<IFileFindResult> GetFilesByPathFilter(string projectRoot, string pathFilter);

        
    }
    public interface IFileFindResult
    {
        string FullPath { get;  }
        string GetContents();
    }
}