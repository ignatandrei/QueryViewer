namespace DB2GUI_RDCG;

[Generator]
public class GeneratorData : ISourceGenerator
{
    public void Execute(GeneratorExecutionContext context)
    {
        var val = context.AnalyzerConfigOptions.GlobalOptions.TryGetValue($"build_property.GenerateStep", out var value);
        if (!val)
        {
            var dd = new DiagnosticDescriptor("noStep", nameof(GeneratorData), $"No GenerateStep", "GenerateStep", DiagnosticSeverity.Error, true);
            var d = Diagnostic.Create(dd, Location.None, "csproj");
            context.ReportDiagnostic(d);
            return;
        }
        switch (value.ToUpper())
        {
            case "CONTEXTANDCLASSES":
                GenerateContextAndClasses(context);
                break;
            case "MODELS":
                GenerateModels(context);
                break;
            case "DBCONTEXT":
                GenerateForContext(context);
                break;
            default:
                var dd = new DiagnosticDescriptor("StepNotFound", nameof(GeneratorData), $"Step not found:"+value,"GenerateStep", DiagnosticSeverity.Error, true);
                var d = Diagnostic.Create(dd, Location.None, "csproj");
                context.ReportDiagnostic(d);
                return;

        }
        
        

    }

    private void GenerateForContext(GeneratorExecutionContext context)
    {
        var gen = context.SyntaxReceiver as DBGeneratorSN;
        var classes = gen.DbContextProps;
        if (classes.Count == 0)
        {
            //not generated yet....
            return;
        }
        var classParent = classes.First().Parent as ClassDeclarationSyntax;
        var nameContext = classParent.Identifier.ValueText;
        var content = EmbedReader.ContentFile("DB2GUI_RSCG.Templates.context.txt"); ;
        var template = Template.Parse(content);
        try
        {
            var rend = template.Render(new
            {
                nameContext,
                queries = classes.Select(it => it.Identifier.ValueText).ToArray(),
            }, member => member.Name);

            context.AddSource("ApplicationDbContextGenerated.cs", SourceText.From(rend, Encoding.UTF8));
        }
        catch (Exception sc)
        {
            string s = sc.Message;
            var dd = new DiagnosticDescriptor("models", nameof(GenerateForContext), $"{sc.Message}", "models", DiagnosticSeverity.Error, true);
            var d = Diagnostic.Create(dd, Location.None, "andrei.txt");
            context.ReportDiagnostic(d);
        }

    }

    private void GenerateModels(GeneratorExecutionContext context)
    {
        var gen = context.SyntaxReceiver as DBGeneratorSN;
        var classes = gen.models;
        if(classes.Count == 0)
        {
            //not generated yet....
            return;
        }
        var classParent = classes.First().Parent as BaseNamespaceDeclarationSyntax;
        var nameContext = classParent.ToString();
        var content = EmbedReader.ContentFile("DB2GUI_RSCG.Templates.ModelToEnum.txt"); ;
        var template = Template.Parse(content);
        var renderClasses = classes.Select(
            it => new
            {
                Name = it.Identifier.ValueText,
                Props = it
                    .Members
                    .Select(it => it as PropertyDeclarationSyntax)
                    .Where(it => it != null)
                    .Select(it => new {
                        Name = it.Identifier.Text,
                        Type = it.Type.ToString(),
                        IsNullable = it.Type.ToString().Contains("?")
                    })
                    .ToArray()
            })
            .GroupBy(it => it.Name)
            .ToDictionary(it => it.Key, v =>
            {
                return v.SelectMany(a => a.Props);
            })
            .Select(it => new
            {
                Name = it.Key,
                Props = it.Value
            })
            .ToArray();
        try
        {
            var rend = template.Render(new
            {
                nameContext,
                classes = renderClasses,
            }, member => member.Name);
            context.AddSource("ModelToEnum.cs", SourceText.From(rend, Encoding.UTF8));
        }
        catch (Exception sc)
        {
            string s = sc.Message;
            var dd = new DiagnosticDescriptor("models", nameof(GenerateModels), $"{sc.Message}", "models", DiagnosticSeverity.Error, true);
            var d = Diagnostic.Create(dd, Location.None, "andrei.txt");
            context.ReportDiagnostic(d);
        }
    }

    string obtainValueAndDiagnostic(string key, GeneratorExecutionContext context)
    {
        var val = context.AnalyzerConfigOptions.GlobalOptions.TryGetValue($"build_property.{key}", out string value);
        if(val)
            return value;

        var dd = new DiagnosticDescriptor("StepNotFound", nameof(GeneratorData), $"Step not found:" + value, "GenerateStep", DiagnosticSeverity.Error, true);
        var d = Diagnostic.Create(dd, Location.None, "csproj");
        context.ReportDiagnostic(d);
        
        return null;

    }
    private void GenerateContextAndClasses(GeneratorExecutionContext context)
    {

        //var ConnectionString = obtainValueAndDiagnostic("ConnectionString", context)??"";
        var Provider = obtainValueAndDiagnostic("Provider", context) ?? "";
        var FolderForContext = obtainValueAndDiagnostic("FolderForContext", context) ?? "";
        var FolderForClasses = obtainValueAndDiagnostic("FolderForClasses", context) ?? "";
        var ProjectWithDesigner = obtainValueAndDiagnostic("ProjectWithDesigner", context);
        if ( Provider.Length * FolderForClasses.Length * FolderForContext.Length * ProjectWithDesigner.Length == 0 )
            return;

        
        var mainSyntaxTree = context.Compilation.SyntaxTrees
                        .First(x => x.HasCompilationUnitRoot);

        var directory = Path.GetDirectoryName(mainSyntaxTree.FilePath);
        Console.WriteLine(directory);
        var startInfo = new ProcessStartInfo();
        startInfo.FileName = @"powershell.exe";
        startInfo.WorkingDirectory = directory;
        string arguments= @"-f create.ps1 ";
        //arguments += $" -connection {ConnectionString}";
        arguments += $" -provider {Provider}";
        arguments += $" -pathToContext {FolderForContext}";
        arguments += $" -pathToModels {FolderForClasses}";
        arguments += $" -project {ProjectWithDesigner}";
        startInfo.Arguments = arguments;
        startInfo.RedirectStandardOutput = true;
        startInfo.RedirectStandardError = true;
        startInfo.UseShellExecute = false;
        startInfo.CreateNoWindow = true;
        var process = new Process();
        process.StartInfo = startInfo;
        process.Start();
        process.WaitForExit(2*60*1000);
        string output = process.StandardOutput.ReadToEnd();
        string errors = (process.StandardError.ReadToEnd()??"");
        
        if (errors.Length > 0 || output.Contains("SqlException"))
        {
            var message ="run create.ps1 with "+ arguments;
            var dd = new DiagnosticDescriptor("PowershellError", nameof(GeneratorData), errors, "powershell", DiagnosticSeverity.Error, true, description:message);
            
            var d = Diagnostic.Create(dd, Location.None, "csproj");
            context.ReportDiagnostic(d);
        }
       
    }

    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(() => new DBGeneratorSN());
    }
}

