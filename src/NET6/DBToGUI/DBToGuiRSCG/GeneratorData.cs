using Scriban;
using System.Linq;

namespace DBToGuiRSCG;

[Generator]
public class GeneratorData : /*ISourceGenerator*/ IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        //Debugger.Launch();        
        GenerateFromContext(context);
        GenerateFromModels(context);
    }
    private void GenerateFromModels(IncrementalGeneratorInitializationContext context)
    {
        var classDeclarations = context.SyntaxProvider.CreateSyntaxProvider
            (
            predicate: (sn, ct) =>
            {
                if (sn is ClassDeclarationSyntax cds)
                {
                    if(sn.Parent is BaseNamespaceDeclarationSyntax ns)
                    {
                        if (ns.Name is IdentifierNameSyntax ins)
                        {
                            return ins.Identifier.Text == "Generated";
                        }
                    }

                }
                return false;
            },
            transform: (gsc, ct) =>
            {
                var pds = gsc.Node as ClassDeclarationSyntax;
                return pds;
            })
            .Where(it => it != null);

        var compilationAndClasses = context.CompilationProvider.Combine(classDeclarations.Collect());

        context.RegisterSourceOutput(compilationAndClasses,
            static (spc, source) => ExecuteForModels(source.Item1, source.Item2, spc));

    }
    private static void ExecuteForModels(Compilation compilation, ImmutableArray<ClassDeclarationSyntax> classes, SourceProductionContext context)
    {
        if (classes.IsDefaultOrEmpty)
        {
            // nothing to do yet
            return;
        }
        if (classes.Any(item => item.Identifier.ValueText == "ApplicationDbContext"))
            return;

        var classParent = classes.First().Parent as BaseNamespaceDeclarationSyntax;
        var nameContext = classParent.ToString();
        var content = EmbedReader.ContentFile("DBToGuiRSCG.Templates.ModelToEnum.txt"); ;
        var template = Template.Parse(content);
        //dealing with partial classes
        var renderClasses = classes.Select(
            it => new
            {
                Name= it.Identifier.ValueText  ,
                Props = it
                    .Members
                    .Select(it => it as PropertyDeclarationSyntax)
                    .Where(it => it != null)
                    .Select(it=>new { Name = it.Identifier.Text, Type = it.Type.ToString() })
                    .ToArray()
            })
            .GroupBy(it=>it.Name)
            .ToDictionary(it=>it.Key,v=>
            {
                return v.SelectMany(a => a.Props);
            })
            .Select(it=> new
            {
                Name=  it.Key,
                Props = it.Value
            })
            .ToArray();
        var rend = template.Render(new
        {
            nameContext,
            classes = renderClasses,
        }, member => member.Name);
        context.AddSource("ModelToEnum.cs", SourceText.From(rend, Encoding.UTF8));
    }

    private void GenerateFromContext(IncrementalGeneratorInitializationContext context)
    {
        var classDeclarations = context.SyntaxProvider.CreateSyntaxProvider
            (
            predicate: (sn, ct) =>
            {
                if (sn is PropertyDeclarationSyntax pds)
                {
                    var ret = pds;
                    return pds.Parent is ClassDeclarationSyntax cds && cds.BaseList?.Types != null && cds.BaseList.Types.Any();
                }
                return false;
            },
            transform: (gsc, ct) =>
            {
                var pds = gsc.Node as PropertyDeclarationSyntax;
                var bt = (pds.Parent as ClassDeclarationSyntax).BaseList.Types;
                if (bt.Any())
                {
                    foreach (var item in bt)
                    {

                        if (item.Type is not IdentifierNameSyntax i)
                            continue;

                        if (i.Identifier.ValueText == "DbContext")
                            return pds;
                    }

                    return null;
                }
                return null;
            })
            .Where(it => it != null);
            

        
        var compilationAndClasses = context.CompilationProvider.Combine(classDeclarations.Collect());

        context.RegisterSourceOutput(compilationAndClasses,
            static (spc, source) => ExecuteForDbSet(source.Item1, source.Item2, spc));

    }
    private static void ExecuteForDbSet(Compilation compilation, ImmutableArray<PropertyDeclarationSyntax> classes, SourceProductionContext context)
    {
        if (classes.IsDefaultOrEmpty)
        {
            // nothing to do yet
            return;
        }
        var classParent = classes.First().Parent as ClassDeclarationSyntax;
        var nameContext = classParent.Identifier.ValueText;
        var content = EmbedReader.ContentFile("DBToGuiRSCG.Templates.context.txt"); ;
        
        var template = Template.Parse(content);
        
        var rend = template.Render(new
        {
            nameContext,
            queries = classes.Select(it => it.Identifier.ValueText).ToArray(),
        }, member => member.Name);
        context.AddSource("ApplicationDbContextGenerated.cs", SourceText.From(rend, Encoding.UTF8));
    }
    //public void Execute(GeneratorExecutionContext context)
    //{
    //    throw new NotImplementedException();
    //}

    //public void Initialize(GeneratorInitializationContext context)
    //{
    //    Debugger.Launch();
    //    throw new NotImplementedException();
    //}
}

