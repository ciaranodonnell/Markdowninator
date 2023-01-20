using System.Diagnostics;

namespace MDDG.Core.Tests
{
    public class CodeFormatterTests : BaseTests
    {
        [Test]
        public void TestRemovesSpacesIndentationSingleLine()
        {
            string input = @"    first line";
            Assert.That(input.RemoveExcessIdentation(), Is.EqualTo("first line"));
        }
        [Test]
        public void TestRemovesSpacesIndentationMultiLine()
        {
            string input = @"    first line
    second line";
            Assert.That(input.RemoveExcessIdentation(), Is.EqualTo(@"first line
second line"));
        }

        [Test]
        public void TestRemovesSpacesIndentationTabs()
        {
            var nl = Environment.NewLine;
            string input = $"\tpublic void a(){nl}\t{{{nl}\t\tthis is the method body{nl}\t}}";

            Assert.That(input.RemoveExcessIdentation(),
            Is.EqualTo($"public void a(){nl}{{{nl}\tthis is the method body{nl}}}"));
        }

        [Test]
        public void TestDoesNothingWithNoIndentSingleLine()
        {
            string input = @"first line";
            Assert.That(input.RemoveExcessIdentation(), Is.EqualTo(input));
        }

        [Test]
        public void TestDoesNothingWithNoIndentMultiLine()
        {
            string input = @"first line
second line";
            Assert.That(input.RemoveExcessIdentation(), Is.EqualTo(input));
        }

    }
}