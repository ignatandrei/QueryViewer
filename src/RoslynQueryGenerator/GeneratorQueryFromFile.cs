using Microsoft.CodeAnalysis.Text;
using Scriban;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace RoslynQueryGenerator
{
    internal class GeneratorQueryFromFile
    {
        private readonly string namespaceName;
        Root root;

        public GeneratorQueryFromFile(string sourceText,string namespaceName)
        {
            
            root = JsonSerializer.Deserialize<Root>(sourceText);
            
            this.namespaceName = namespaceName;
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
            string pathFolder = @"E:\ignatandrei\QueryViewer\src\QueryViewerWebAPI";
            string file = Path.Combine(pathFolder, name);
            var template = Template.Parse(File.ReadAllText(file));
            var context = template.Render(new
            {
                this.namespaceName,
                this.root
            }, member => member.Name);

            return context;
        }
        internal string GenerateContext()
        {

            return GenerateFromFile("templates/DBContextTemplate.txt");
            
            
        }

        internal string GenerateExtensions()
        {
            return GenerateFromFile("templates/Extensions.txt");
            
        }

        internal string GenerateController()
        {
            return GenerateFromFile("templates/Controller.txt");
            
        }
        internal string GenerateFind()
        {
            return GenerateFromFile("templates/SearchClasses.txt");
        }
    }
}