using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDDG.Core.FileFinding
{
    public class SimpleFileFinder : IFileFinder
    {
        public List<IFileFindResult> GetFilesByPathFilter(string projectRoot, string pathFilter)
        {
            DirectoryInfo di = new DirectoryInfo(projectRoot);

            if (!di.Exists) { return new(); }

            return di.GetFiles(pathFilter, SearchOption.AllDirectories).Select(f => new SimpleFindFileResult(f) as IFileFindResult).ToList();

        }



        private class SimpleFindFileResult : IFileFindResult
        {
            private readonly FileInfo file;

            public SimpleFindFileResult(FileInfo file)
            {
                this.file = file;
            }
            public string FullPath => file.FullName;

            public string GetContents()
            {
                using (var sr = new StreamReader(file.FullName))
                    return sr.ReadToEnd();
            }
        }
    }
}
