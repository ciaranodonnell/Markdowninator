using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Markdowninator.Core.FilesFinding
{
    internal class MockFileFinder : IFileFinder
    {
        private readonly Directory directory;

        public class File
        {            public string Name { get; set; }    
            public string Contents { get; set; }
        }

        public class Directory
        {
            public string Name { get; set; }
            public List<File> Files { get; set; }
            public List<Directory> Directories{ get; set; }

            public IEnumerable<File> GetFiles(string pattern)
            {
                var files = new List<File>();
                var re = pattern.Replace("*", "[A-Za-z0-1]*");
                foreach(var file in this.Files)
                {
                    if (Regex.IsMatch(file.Name, re))
                    {
                        files.Add(file);
                    }
                }
                return files;
            }
        }

        private class FileFindResult : IFileFindResult
        {
            private readonly File file;

            public FileFindResult(File file)
            {
                this.file = file;
            }

            public string FullPath { get { return file.Name; } }

            public string GetContents()
            {
                return file.Contents;
            }
        }

        public MockFileFinder(Directory directory)
        {
            this.directory = directory;
        }

        public List<IFileFindResult> GetFilesByPathFilter(string projectRoot, string pathFilter)
        {
            if (projectRoot == this.directory.Name)
            {
                var pathParts = pathFilter.Split(new char[] {'/', '\\'}, StringSplitOptions.None).ToList();
                if (pathParts.Count > 1)
                {
                    var dir = directory;
                    for(int x=0;x<pathParts.Count-1;x++) {
                        foreach(var subDir in dir.Directories)[x].Name == pathParts[x];
                    }
                }
            }
            
            
                return new List<IFileFindResult>();
            
        }
    }
}
