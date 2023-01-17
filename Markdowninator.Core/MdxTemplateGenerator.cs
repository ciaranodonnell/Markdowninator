
using MDDG.Interface;
using Mono.TextTemplating;
namespace MDDG.Core;

public class MdxTemplateGenerator : TemplateGenerator, IMarkdowninatorContainer
{

    public MdxTemplateGenerator(IMarkdowninator md)
    {
        this.Markdowninator = md;
    }
    public IMarkdowninator Markdowninator { get; init; }

}