
using System.Diagnostics;
using System.IO;

namespace DB2GUI_RSCG;
[Generator(LanguageNames.CSharp)]
public class GeneratorData : IIncrementalGenerator
{
    public string x { get; set; }

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {


        var additionalTexts = context.AdditionalTextsProvider;
        //// filter additional texts by extension
        var cnDetails= additionalTexts
            .Where((text) =>
            {
                return text.Path.EndsWith("connectionDetails.txt" , StringComparison.OrdinalIgnoreCase);
            })
            .Select((it,data)=>
            {
                return new { path = it.Path, text = it.GetText(data) };
            });

        var all = cnDetails;//.Combine(cp);
        context.RegisterSourceOutput(all, (spc, source) =>
        {
            var pathGenerateFromDB = source.path;
            var directory = Path.GetDirectoryName(pathGenerateFromDB);

            string text = source.text.ToString();

            var root= JsonConvert.DeserializeObject<Root>(text);
            int nrCon = 0;
            foreach (var item in root.connections)
            {
                nrCon++;
                if (!item.Enabled)
                    continue;
                var Provider = item.Provider ?? "";
                var ProjectForContext = item.ProjectForContext ?? "";
                var ProjectForClasses = item.ProjectForClasses ?? "";
                var ProjectWithDesigner = item.ProjectWithDesigner ?? "";
                var contextName = item.NameContext ?? "";
                var connection = item.ConnectionString ?? "";
                if (connection.Length * contextName.Length * Provider.Length * ProjectForClasses.Length * ProjectForContext.Length * ProjectWithDesigner.Length == 0)
                {
                    var dd = new DiagnosticDescriptor($"missingData for connection {nrCon}", nameof(GeneratorData), $"Please verify file with connection {nrCon}", $"Please verify file with connection {nrCon}", DiagnosticSeverity.Error, true);
                    var d = Diagnostic.Create(dd, Location.None, "csproj");
                    spc.ReportDiagnostic(d);
                    continue;
                }
                foreach(var errMessage in RunPowerShell(item, directory))
                {

                    var dd = new DiagnosticDescriptor($"error for connection {nrCon}", nameof(GeneratorData), errMessage, $"Please verify file with connection {nrCon}", DiagnosticSeverity.Error, true);
                    var d = Diagnostic.Create(dd, Location.None, "csproj");
                    spc.ReportDiagnostic(d);


                }
            }

        } );
        //            )
        //            return value;

        //        return string.Empty;
        //        ;
        //    });
        ////context.RegisterImplementationSourceOutput
        //context.RegisterSourceOutput(value, (spc, source) =>
        //{
        //    var valueToRun = source;
        //    switch (valueToRun.ToUpper())
        //    {
        //        case "":
        //            return;
        //        case "MODELS":
        //            return;
        //        default:
        //            var dd = new DiagnosticDescriptor("StepNotFound", nameof(GeneratorData), $"Step not found:" + valueToRun, "GenerateStep", DiagnosticSeverity.Error, true);
        //            var d = Diagnostic.Create(dd, Location.None, "csproj");
        //            spc.ReportDiagnostic(d);
        //            return;
        //    }
        //});

    }
    private IEnumerable<string> RunPowerShell(Connection item,string directory)
    {
        var file = Path.Combine(directory, "create.ps1");
        var startInfo = new ProcessStartInfo();
        startInfo.FileName = @"powershell.exe";
        startInfo.WorkingDirectory = directory;
        string arguments = @"-NoProfile -NonInteractive -ExecutionPolicy ByPass ";
        arguments += $"""
            -f "{file}"  
            """; 
        //arguments += $" -connection {ConnectionString}";
        arguments += $" -provider {item.Provider}";
        arguments += $" -projectContext {item.ProjectForContext}";
        arguments += $" -projectModels {item.ProjectForClasses}";
        arguments += $" -projectWeb {item.ProjectWithDesigner}";
        arguments += $" -nameContext {item.NameContext}";
        arguments += $" -connection \"{item.ConnectionString}\"";
        startInfo.Arguments = arguments;        
        startInfo.RedirectStandardOutput = true;
        startInfo.RedirectStandardError = true;
        startInfo.UseShellExecute = false;
        startInfo.CreateNoWindow = true;
        var process = new Process();
        process.StartInfo = startInfo;
        string output = "", errors = "";
        process.OutputDataReceived += (sender, e) => { if (e.Data != null) output += e.Data; };
        process.ErrorDataReceived += (sender, e) => { if (e.Data != null) errors += e.Data; };

        process.Start();

        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
        process.WaitForExit();

        //string output = process.StandardOutput.ReadToEnd();
        //string errors = (process.StandardError.ReadToEnd()??"");

        if (errors.Length > 0 || output.Contains("SqlException") || output.Contains("Build failed"))
        {
            yield return "run powershell with " + arguments;
            var tempFile = Path.GetTempFileName() + ".txt";
            File.WriteAllText(tempFile, output + errors);
            var message = tempFile;
            yield return tempFile;   
        }
    }

}

