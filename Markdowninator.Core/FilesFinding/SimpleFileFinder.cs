using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markdowninator.Core.FilesFinding
{
    internal class SimpleFileFinder : IFileFinder
    {
        public List<string> GetFilesByPathFilter(string projectRoot, string pathFilter)
        {
            DirectoryInfo di = new DirectoryInfo(projectRoot);
            
            if (!di.Exists ) { return new List<string>(); }


            return di.GetFiles(pathFilter, SearchOption.AllDirectories).Select(f=> f.FullName).ToList();

        }
    }
}
