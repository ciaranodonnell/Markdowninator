using MDDG.Core.FileFinding;
using MDDG.Interface;
using Mono.TextTemplating;
using System.Linq;

namespace MDDG.Core;

public class MdxTemplateRunner
{
    private readonly string tempDirectory;
    private readonly string projectRootDirectory;

    public MdxTemplateRunner(string tempDirectory, string projectRootDirectory)
    {
        this.tempDirectory = tempDirectory;
        this.projectRootDirectory = projectRootDirectory;
    }


    public async Task<List<string>> GenerateTemplate(string mdxFile)
    {
        var templateFilename = Path.Combine(tempDirectory, Path.GetFileName(mdxFile));

        string templateContent = File.ReadAllText(templateFilename);

        var generator = new MdxTemplateGenerator(new Markdowninator(projectRootDirectory, new SimpleFileFinder()));

        templateContent += @"<#+ 
        public Markdowninator MD { get { 
        return (Markdowninator)
            Host.GetType().GetProperty(""Markdowninator"", 
            System.Reflection.BindingFlags.Instance | 
            System.Reflection.BindingFlags.Public | 
            System.Reflection.BindingFlags.DeclaredOnly
            ).GetValue(Host);
        }} #>";

        ParsedTemplate parsed = generator.ParseTemplate(templateFilename, templateContent);

        TemplateSettings settings = TemplatingEngine.GetSettings(generator, parsed);
        settings.HostSpecific = true;

        generator.Refs.Add(typeof(MdxTemplateGenerator).Assembly.Location);
        generator.Refs.Add(typeof(IMarkdowninator).Assembly.Location);
        generator.Imports.Add(typeof(MdxTemplateGenerator).Namespace);
        generator.Imports.Add(typeof(IMarkdowninator).Namespace);

        settings.CompilerOptions = "-nullable:enable";


        (string generatedFilename, string generatedContent) = await generator.ProcessTemplateAsync(
            parsed, templateFilename, templateContent, templateFilename, settings
        );

        File.WriteAllText(generatedFilename, generatedContent);

        List<string> errors = new();
        if (generator.Errors.HasErrors)
        {
            foreach (var error in generator.Errors)
                errors.Add(error.ToString());
        }

        return errors;
    }


}