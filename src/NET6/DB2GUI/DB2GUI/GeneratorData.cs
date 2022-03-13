

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
            default:
                var dd = new DiagnosticDescriptor("StepNotFound", nameof(GeneratorData), $"Step not found:"+value,"GenerateStep", DiagnosticSeverity.Error, true);
                var d = Diagnostic.Create(dd, Location.None, "csproj");
                context.ReportDiagnostic(d);
                return;

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

        if ( Provider.Length * FolderForClasses.Length * FolderForContext.Length == 0)
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
        startInfo.Arguments = arguments;
        startInfo.RedirectStandardOutput = true;
        startInfo.RedirectStandardError = true;
        startInfo.UseShellExecute = false;
        startInfo.CreateNoWindow = false;
        var process = new Process();
        process.StartInfo = startInfo;
        process.Start();
        process.WaitForExit(1*60*1000);
        string output = process.StandardOutput.ReadToEnd();
        string errors = (process.StandardError.ReadToEnd()??"");
        if (errors.Length > 0)
        {

            var dd = new DiagnosticDescriptor("PowershellError", nameof(GeneratorData), errors, "powershell", DiagnosticSeverity.Error, true);
            var d = Diagnostic.Create(dd, Location.None, "csproj");
            context.ReportDiagnostic(d);
        }
    }

    public void Initialize(GeneratorInitializationContext context)
    {
        
    }
}

