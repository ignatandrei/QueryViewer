using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Scriban;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace RoslynQueryGenerator
{
    internal class GeneratorQueryFromFile
    {
        private readonly string namespaceName;
        Root root;
        private ImmutableArray<AdditionalText> additionalFiles;

        

        public GeneratorQueryFromFile(GeneratorExecutionContext context, string namespaceName)
        {
            this.additionalFiles = context.AdditionalFiles;
            this.namespaceName = namespaceName;
            var query = additionalFiles.Where(
                   file =>
                   {
                       var f = context.AnalyzerConfigOptions.GetOptions(file);
                       if (!f.TryGetValue("build_metadata.AdditionalFiles.generateQuery", out string val))
                           return false;

                       return (val == "true");
                   }
                   ).FirstOrDefault();

            root = JsonSerializer.Deserialize<Root>(query.GetText().ToString());

        }

        private void GenerateData()
        {
            foreach (var item in root.itemData)
            {
                if (!item.HasResolvedQueries)
                {
                    item.ResolveQueries();
                }
            }
        }
        private string GenerateFromFile(string name)
        {
            GenerateData();
            //string pathFolder = @"E:\ignatandrei\QueryViewer\src\QueryViewerWebAPI";
            //string file = Path.Combine(pathFolder, name);
            //var template = Template.Parse(File.ReadAllText(file));
            var content = additionalFiles.Where(it =>it.Path.Contains(name)).FirstOrDefault().GetText().ToString();
            var template = Template.Parse(content);
            var context = template.Render(new
            {
                this.namespaceName,
                this.root
            }, member => member.Name);

            return context;
        }
        internal string GenerateContext()
        {

            return GenerateFromFile(@"DBContextTemplate.txt");
            
            
        }

        internal string GenerateExtensions()
        {
            return GenerateFromFile(@"Extensions.txt");
            
        }

        internal string GenerateController()
        {
            return GenerateFromFile(@"Controller.txt");
            
        }
        internal string GenerateFind()
        {
            return GenerateFromFile(@"SearchClasses.txt");
        }
    }
}