using System.Diagnostics;
using Markdowninator.Core.FileFinding;

namespace Markdowninator.Core.Tests
{
    public class SimpleFileFinderTests : BaseTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CanFindFileByName()
        {
            var finder = new SimpleFileFinder();

            var projRoot = Path.GetDirectoryName(typeof(SimpleFileFinderTests).Assembly.Location);
            var files = finder.GetFilesByPathFilter(
                projRoot,
                "File21.cs");

            Assert.AreEqual(1, files.Count);
            Assert.AreEqual(CodeFilePath("/Directory2/File21.cs"), files[0].FullPath);


        }


        [Test]
        public void CanGetFileContents()
        {
            var finder = new SimpleFileFinder();

            var projRoot = Path.GetDirectoryName(typeof(SimpleFileFinderTests).Assembly.Location);
            var files = finder.GetFilesByPathFilter(
                projRoot,
                "File21.cs");

            var filepath = CodeFilePath("/Directory2/File21.cs");
            Assert.AreEqual(File.ReadAllText(filepath), files[0].GetContents());
        }
    }
}