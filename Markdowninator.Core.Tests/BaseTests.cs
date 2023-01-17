using System.Diagnostics;
using Markdowninator.Core.FileFinding;

namespace Markdowninator.Core.Tests
{
    public class BaseTests
    {

        private string ProjectRoot
        {
            get
            {
                return Path.GetDirectoryName(typeof(SimpleFileFinderTests).Assembly.Location);
            }
        }
        public string TestContentPath { get { return Path.Combine(ProjectRoot, "TestContent"); } }
        public string CodePath { get { return Path.Combine(TestContentPath, "Code"); } }
        public string DocsPath { get { return Path.Combine(TestContentPath, "Docs"); } }

        public string DocsFilePath(string filePath)
        {
            return Path.Combine(DocsPath, filePath.Replace('/', Path.DirectorySeparatorChar));
        }
        public string CodeFilePath(string filePath)
        {
            return Path.Combine(CodePath, filePath.Replace('/', Path.DirectorySeparatorChar));
        }

    }
}
