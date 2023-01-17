using Markdowninator.Interface;
using Mono.TextTemplating;
using System.Linq;

namespace Markdowninator.Core;

public class MdxTemplateRunner
{
    private readonly string tempDirectory;

    public MdxTemplateRunner(string tempDirectory)
    {
        this.tempDirectory = tempDirectory;
    }


    public async Task<List<string>> GenerateTemplate(string mdxFile)
    {
        var templateFilename = Path.Combine(tempDirectory, Path.GetFileName(mdxFile));

        string templateContent = File.ReadAllText(templateFilename);

        var generator = new MdxTemplateGenerator(new Markdowninator());
        //string.Join(", ", GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.DeclaredOnly).Select(p => p.Name));
        //templateContent += $"<#+ public /*{typeof(IMarkdowninator).Name}*/ string Markdowninator {{ get {{ return string.Join(\", \", Host.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.DeclaredOnly).Select(p => p.Name)); /*.GetProperty(\"Markdowninator\",System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public).GetValue(this.Host) as IMarkdowninator;*/}}}} #>";

        templateContent = @"
<#@ assembly name=""" + typeof(MdxTemplateGenerator).Assembly.Location + @""" #>
<#@ assembly name=""" + typeof(IMarkdowninatorContainer).Assembly.Location + @""" #>
<#@ import namespace=""" + typeof(MdxTemplateGenerator).Namespace + @""" #>
<#@ import namespace=""" + typeof(IMarkdowninatorContainer).Namespace + @""" #>
" + templateContent;

        templateContent += @"<#+ 
        public object Markdowninator { get { 
           // throw new Exception($""{Host.GetType().Assembly.Location} - {typeof(MdxTemplateGenerator).Assembly.Location}"");
        return ((MdxTemplateGenerator)Host).Markdowninator;}} #>";

        ParsedTemplate parsed = generator.ParseTemplate(templateFilename, templateContent);

        TemplateSettings settings = TemplatingEngine.GetSettings(generator, parsed);
        settings.HostSpecific = true;
        /*
        generator.Refs.Add(typeof(MdxTemplateGenerator).Assembly.Location);
        generator.Refs.Add(typeof(IMarkdowninator).Assembly.Location);
        generator.Imports.Add("Markdowninator.Core");
        generator.Imports.Add("Markdowninator.Interface");
        */
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