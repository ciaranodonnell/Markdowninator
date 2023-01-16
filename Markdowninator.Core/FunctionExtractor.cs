using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Markdowninator.Core
{
    public class FunctionExtractor
    {
        private readonly IFileFinder fileFinder;

        public FunctionExtractor(IFileFinder fileFinder)
        {
            this.fileFinder = fileFinder;
        }

        public string[] GetFunctionBody(string projectRoot, string pathFilter, string className,  string functionName)
        {
            List<string> functions = new List<string>();


            List<string> files = fileFinder.GetFilesByPathFilter(projectRoot, pathFilter);


            foreach(string file in files) {
                var code = new StreamReader(file).ReadToEnd();
                SyntaxTree tree = CSharpSyntaxTree.ParseText(code);

                var methods = tree.GetRoot().DescendantNodes().OfType<MethodDeclarationSyntax>().ToList();
                foreach (var method in methods)
                {
                    if (method.Identifier.Text)
                }
            }



            return functions.ToArray();

        }

    }
}
