﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

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
                {
                    var template = FromValue(context,value);
                    GenerateModels(context, template);
                }
                break;
            case "CONTEXT":
                {
                    var template = FromValue(context, value);
                    GenerateForContext(context, template);
                }
                break;
            case "CONTROLLER":
                {
                    var template = FromValue(context, value);
                    GenerateControllers(context,template);
                }
                break;
            default:
                var dd = new DiagnosticDescriptor("StepNotFound", nameof(GeneratorData), $"Step not found:"+value,"GenerateStep", DiagnosticSeverity.Error, true);
                var d = Diagnostic.Create(dd, Location.None, "csproj");
                context.ReportDiagnostic(d);
                return;

        }
        
        

    }

    private Template FromValue(GeneratorExecutionContext context, string val)
    {
        try
        {
            val = val.ToLower();
            var content = "";
            var file = context.AdditionalFiles.Where(it => it.Path.EndsWith($"{val}.txt"))
                    .Select(it => it.GetText())
                    .FirstOrDefault();

            if (file != null)
            {
                content = string.Join(Environment.NewLine, file.Lines.Select(it => it.ToString()));
            }

            if (string.IsNullOrWhiteSpace(content))
            {
                content = EmbedReader.ContentFile($"DB2GUI_RSCG.Templates.{val}.txt"); ;
            }
            var template = Template.Parse(content);
            return template;
        }
        catch (Exception ex)
        {

            string s = ex.Message;
            var dd = new DiagnosticDescriptor("FromValue", nameof(FromValue), $"{ex.Message}", "models", DiagnosticSeverity.Error, true);
            var d = Diagnostic.Create(dd, Location.None, $"{val}.txt");
            context.ReportDiagnostic(d);
            throw;
        }
    }
    

    private void GenerateControllers(GeneratorExecutionContext context, Template template)
    {
        var gen = context.SyntaxReceiver as DBGeneratorSN;
        var contexts = gen.dbcontext;
        foreach (var item in contexts)
        {
            var data = context.Compilation.GetSemanticModel(item.SyntaxTree);
            var ti= data.GetTypeInfo(item);
            //var orig = ti.ConvertedType;
            //var morig = orig.GetMembers().First();
            //var moirig = orig.GetTypeMembers();

            var tn = ti.Type;
            var members= tn.GetMembers();
            
            foreach (var member in members)
            {

                if (member is IPropertySymbol ps)
                {
                    
                    var typeDbSet = ps.Type;
                    INamedTypeSymbol a =typeDbSet as INamedTypeSymbol;
                    var model = a.TypeArguments.First() as INamedTypeSymbol;
                    var membersModel=model .GetMembers();
                    List< IPropertySymbol > keys=new List<IPropertySymbol>();
                    List<IPropertySymbol> cols = new List<IPropertySymbol>();
                    foreach (var m in membersModel)
                    {
                        if (m is not IPropertySymbol ips)
                            continue;
                        if (ips.Type.Name == "ICollection")
                            continue;
                        cols.Add(ips);
                        var atts = m.GetAttributes();
                        if (atts.Length == 0)
                            continue;
                        foreach(var att in atts)
                        {
                            var cls= att.AttributeClass as INamedTypeSymbol;
                            var s = cls?.Name;
                            if(s == "Key" || s=="KeyAttribute")
                            {
                                keys.Add(ips);
                                continue;
                            }

                            
                        }
                    }

                    var t=new { Name =ps.Name};
                    var rend = template.Render(new
                    {
                        table = t ,
                        numberKeys=keys.Count,
                        keys=keys.Select(it=>new { it.Name, TypeName= it.Type.Name }).ToArray(),
                        firstKey = new
                        {
                            keys.FirstOrDefault()?.Name,
                            TypeName = keys.FirstOrDefault()?.Type?.Name
                        },
                        cols = cols.Select(it => new { it.Name, TypeName = it.Type.Name }).ToArray(),
                    }, member => member.Name);

                    context.AddSource($"{ps.Name}Generated.cs", SourceText.From(rend, Encoding.UTF8));
                }



            }
            //var tn2 = tn.GetTypeMembers();

            //var a = data.GetSymbolInfo(item);
            //var s2 = a.Symbol;
            
            //var s = context.Compilation.GetTypeByMetadataName(orig.ToDisplayString());
            //var m = s.GetTypeMembers();
            //var m1=s.GetMembers().First();            
            var str = "1";
            str += "2";
        }
    }

    private void GenerateForContext(GeneratorExecutionContext context,Template template)
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

    private void GenerateModels(GeneratorExecutionContext context, Template template)
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
            var message ="run powershell with "+ arguments;
            var dd = new DiagnosticDescriptor("PowershellError",message, message, "powershell", DiagnosticSeverity.Error, true, description:message);
            
            var d = Diagnostic.Create(dd, Location.None, "csproj");
            context.ReportDiagnostic(d);
        }
       
    }

    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(() => new DBGeneratorSN());
    }
}

