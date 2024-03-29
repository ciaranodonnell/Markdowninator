using System.Diagnostics;
using MDDG.Core.FileFinding;

namespace MDDG.Core.Tests
{
    public class MdxTemplateGeneratorTests : BaseTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GenerateSimpleTemplate()
        {

            var docsFolder = this.DocsPath;
            var inputPath = DocsFilePath("GenerateSimpleTemplate.mdx");
            var outputPath = DocsFilePath("GenerateSimpleTemplate.md");


            if (File.Exists(outputPath)) File.Delete(outputPath);

            Assert.That(File.Exists(inputPath), $"Input file {inputPath} should exist");

            var generator = new MdxTemplateRunner(tempDirectory: docsFolder, this.TestContentPath);

            var errors = await generator.GenerateTemplate(inputPath);
            if (errors.Count > 0) Assert.Fail(errors[0]);
            Assert.That(File.Exists(outputPath), "Output path should now exist");

        }


    }
}