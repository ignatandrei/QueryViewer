
namespace DB2GUI_RDCG;
[Generator]
public class GeneratorData : ISourceGenerator
{
    private Root root;
    public void Execute(GeneratorExecutionContext context)
    {
        
        var val = context.AnalyzerConfigOptions.GlobalOptions.TryGetValue($"build_property.GenerateStep", out var value);
        if (!val)
        {
            var dd = new DiagnosticDescriptor("noStep", nameof(GeneratorData), $"No GenerateStep", "GenerateStep", DiagnosticSeverity.Warning, true);
            var d = Diagnostic.Create(dd, Location.None, "csproj");
            context.ReportDiagnostic(d);
            return;
        }
        switch (value.ToUpper())
        {
            case "NOSTEP":
                return;
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
                    {
                        var dd = new DiagnosticDescriptor("test", nameof(GeneratorData), $"test" + value, "GenerateStep", DiagnosticSeverity.Warning, true);
                        var d = Diagnostic.Create(dd, Location.None, "csproj");
                        context.ReportDiagnostic(d);
                    }
                    try
                    {
                        var template = FromValue(context, value);
                        GenerateControllers(context, template);
                    }
                    catch(Exception ex)
                    {
                        {
                            string s = ex.Message + ex.StackTrace;
                            var dd = new DiagnosticDescriptor("FromValue", nameof(FromValue), $"{ex.StackTrace}", "models", DiagnosticSeverity.Error, true);
                            var d = Diagnostic.Create(dd, Location.None, $"{val}.txt");
                            context.ReportDiagnostic(d);
                        }
                    }
                }
                break;
            default:
                {
                    var dd = new DiagnosticDescriptor("StepNotFound", nameof(GeneratorData), $"Step not found:" + value, "GenerateStep", DiagnosticSeverity.Error, true);
                    var d = Diagnostic.Create(dd, Location.None, "csproj");
                    context.ReportDiagnostic(d);
                    return;
                }

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
            var nameContext = tn.Name;
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
                        nameContext,
                        table = t ,
                        numberKeys=keys.Count,
                        keys=keys.Select(it=>new { it.Name, TypeName= (it.Type as INamedTypeSymbol).ToDisplayString() }).ToArray(),
                        firstKey = new
                        {
                            keys.FirstOrDefault()?.Name,
                            TypeName = keys.FirstOrDefault()?.Type?.Name
                        },
                        cols = cols.Select(it => new { it.Name, TypeName = (it.Type as INamedTypeSymbol)?.ToDisplayString() }).ToArray(),
                    }, member => member.Name);

                    context.AddSource($"{nameContext}_{ps.Name}_Generated.cs", SourceText.From(rend, Encoding.UTF8));
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
    string GeneraLText()
    {
        return @"

namespace Generated;
public class OrderBy<TColumn>
    where TColumn : System.Enum
{
    public TColumn? FieldName { get; set; }
    public bool Asc { get; set; }
}
public class SearchField<TColumn>
    where TColumn : System.Enum
{
    public TColumn? FieldName { get; set; }        
    public string? Value { get; set; }
    public SearchCriteria Criteria { get; set; }
         
    //public string? CriteriaString { get; set; }

}
public abstract class Search<TColumn, TClass>
    where TColumn : System.Enum
    where TClass: class
{
    public Search(){
        PageSize=10;
        PageNumber=1;
    }
    public SearchField<TColumn>[]? SearchFields { get; set; }
    public OrderBy<TColumn>[]? OrderBys { get; set; }
    public int PageSize{get;set;}
    public int PageNumber{get;set;}
    public abstract IQueryable<TClass> TransformToWhere(IQueryable<TClass> data);
    public abstract IOrderedQueryable<TClass> TransformToOrder(IQueryable<TClass> data);
    //public abstract IOrderedQueryable<TClass> TransformToPaging(IOrderedQueryable<TClass>  data);

}

public enum SearchCriteria
{
    None = 0,
    StartsWith,
    EndsWith,
    Contains,
    Equal,
    InArray,
    NotInArray,
    Between,
    NotBetween,
    Different,
    Greater,
    Less,
    GreaterOrEqual,
    LessOrEqual,
    //Like,

    EqualYear,
    DifferentYear,
    GreaterYear,
    LessYear,
    GreaterOrEqualYear,
    LessOrEqualYear,

    EqualMonthYear,
    DifferentMonthYear,
    GreaterMonthYear,
    LessMonthYear,
    GreaterOrEqualMonthYear,
    LessOrEqualMonthYear,
    
    EqualDay,
    DifferentDay,
    GreaterDay,
    LessDay,
    GreaterOrEqualDay,
    LessOrEqualDay,
    
}
";
    }
    private void GenerateForContext(GeneratorExecutionContext context,Template template)
    {
        context.AddSource("general.cs", GeneraLText());
        var gen = context.SyntaxReceiver as DBGeneratorSN;
        var classes = gen.DbContextProps;
        if (classes.Count == 0)
        {
            //todo: make warning
            return;
        }
        foreach (var classParent in classes)
        {
            var data = context.Compilation.GetSemanticModel(classParent.SyntaxTree);


            //var classParent = classes.First().Parent as ClassDeclarationSyntax;
            var nameContext = classParent.Identifier.ValueText;
            var dbSets = classParent.Members
                    .Where(it=>it is PropertyDeclarationSyntax )
                    .Select(it=>it as PropertyDeclarationSyntax)
                    .ToArray();
            if (!dbSets.Any())
            {
                //todo: make warning
                continue;
            }
            var tables=dbSets
                .Select(it=>it.Type as GenericNameSyntax)
                .Where(it=>it !=null)
                .Select(it=>it.TypeArgumentList)
                .Where(it => it != null)
                .Select(it=>it.Arguments)
                .Where(it => it != null)
                .Select(it => it.FirstOrDefault())
                .Where(it => it != null)

                .Select(it => data.GetTypeInfo(it))
                .Select(it => new Table(it))                
                .ToArray();



            var test = tables.FirstOrDefault();
            //var realDbSets = dbSets;
            //var set = dbSets.First();
            //var gns = set.Type as GenericNameSyntax;
            //var type = gns.TypeArgumentList.Arguments.FirstOrDefault();
            //var ti = data.GetTypeInfo(type);
            //var m = ti.Type.GetMembers();
            var dataToRender = new
            {
                nameContext,
                queries = tables.Select(it => new
                {
                    Name = it.Name,
                    numberKeys = it.keys.Length,
                    keys = it.keys.ToArray(),
                    firstKey = it.keys.FirstOrDefault(),
                    cols = it.cols.ToArray(),


                }).ToArray()
            };
            var q = dataToRender.queries.FirstOrDefault();
            try
            {
                var rend = template.Render(dataToRender, member => member.Name);

                context.AddSource($"{nameContext}Generated.cs", SourceText.From(rend, Encoding.UTF8));
            }
            catch (Exception sc)
            {
                string s = sc.Message;
                var dd = new DiagnosticDescriptor("models", nameof(GenerateForContext), $"{sc.Message}", "models", DiagnosticSeverity.Error, true);
                var d = Diagnostic.Create(dd, Location.None, "andrei.txt");
                context.ReportDiagnostic(d);
            }
        }
    }

    private void GenerateModels(GeneratorExecutionContext context, Template template)
    {
        var gen = context.SyntaxReceiver as DBGeneratorSN;
        var classes = gen.models;
        if (classes.Count == 0)
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
                        RawKindType= it.Type.Kind(),
                        IsArray= it.Type.ToString().Contains("[]"),
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
        var file = context.AdditionalFiles.Where(it => it.Path.Contains("connectionDetails")).FirstOrDefault();
        if (file == null)
        {
            var dd = new DiagnosticDescriptor("connectionDetails", nameof(GeneratorData), $"No connectionDetails", "No connectionDetails", DiagnosticSeverity.Error, true);
            var d = Diagnostic.Create(dd, Location.None, "csproj");
            context.ReportDiagnostic(d);
            return;
        }
        var text = file.GetText().ToString();

        root = JsonConvert.DeserializeObject<Root>(text);
        //root = new Root();

        //var ConnectionString = obtainValueAndDiagnostic("ConnectionString", context)??"";
        //var Provider = obtainValueAndDiagnostic("Provider", context) ?? "";
        //var FolderForContext = obtainValueAndDiagnostic("FolderForContext", context) ?? "";
        //var FolderForClasses = obtainValueAndDiagnostic("FolderForClasses", context) ?? "";
        //var ProjectWithDesigner = obtainValueAndDiagnostic("ProjectWithDesigner", context);
        //var contextName = obtainValueAndDiagnostic("NameContext", context);
        int nrCon= 0;
        foreach(var item in root.connections) { 
            nrCon++;
            var Provider = item.Provider??"";
            var FolderForContext = item.FolderForContext ?? "";
            var FolderForClasses = item.FolderForClasses ?? "";
            var ProjectWithDesigner = item.ProjectWithDesigner ?? "";
            var contextName = item.NameContext ?? "";
            var connection = item.ConnectionString ?? "";
            if (connection.Length * contextName.Length * Provider.Length * FolderForClasses.Length * FolderForContext.Length * ProjectWithDesigner.Length == 0)
            {
                var dd = new DiagnosticDescriptor($"missingData for connection {nrCon}", nameof(GeneratorData), $"Please verify file with connection {nrCon}", $"Please verify file with connection {nrCon}", DiagnosticSeverity.Error, true);
                var d = Diagnostic.Create(dd, Location.None, "csproj");
                context.ReportDiagnostic(d);
                return;
            }

        
        var mainSyntaxTree = context.Compilation.SyntaxTrees
                        .First(x => x.HasCompilationUnitRoot);

        var directory = Path.GetDirectoryName(mainSyntaxTree.FilePath);
        Console.WriteLine(directory);
        var startInfo = new ProcessStartInfo();
        startInfo.FileName = @"powershell.exe";
        startInfo.WorkingDirectory = directory;
        string arguments= @"-NoProfile -NonInteractive -ExecutionPolicy ByPass ";
        arguments += @" -f create.ps1  ";
        //arguments += $" -connection {ConnectionString}";
        arguments += $" -provider {Provider}";
        arguments += $" -pathToContext {FolderForContext}";
        arguments += $" -pathToModels {FolderForClasses}";
        arguments += $" -project {ProjectWithDesigner}";
        arguments += $" -nameContext {contextName}";
        arguments += $" -connection \"{connection}\"";
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

                var message = "run powershell with " + arguments;
                var dd = new DiagnosticDescriptor("PowershellError", message, message, "powershell", DiagnosticSeverity.Error, true, description: message);

                var d = Diagnostic.Create(dd, Location.None, "csproj");
                context.ReportDiagnostic(d);
                var tempFile = Path.GetTempFileName() + ".txt";
                File.WriteAllText(tempFile, output + errors);
                message = tempFile;

                dd = new DiagnosticDescriptor("PowershellError", message, message, "powershell", DiagnosticSeverity.Error, true, description: message);

                d = Diagnostic.Create(dd, Location.None, "csproj");
                context.ReportDiagnostic(d);
                try
                {
                    //Process.Start("notepad.exe", tempFile);
                }
                catch (Exception)
                {

                }
            }
        }

    }

    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(() => new DBGeneratorSN());
    }
}

