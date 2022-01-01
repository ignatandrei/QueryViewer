namespace DBToGuiRSCG;

[Generator]
public class GeneratorData : /*ISourceGenerator*/ IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        Debugger.Launch();

        var classDeclarations = context.SyntaxProvider.CreateSyntaxProvider
            (
            predicate: (sn, ct) =>
            {
                if (sn is PropertyDeclarationSyntax pds)
                {
                    var ret = pds;
                    return pds.Parent is ClassDeclarationSyntax cds && cds.BaseList.Types.Any();
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

                        if (!(item.Type is IdentifierNameSyntax i))
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
            static (spc, source) => Execute(source.Item1, source.Item2, spc));
    }
    private static void Execute(Compilation compilation, ImmutableArray<PropertyDeclarationSyntax> classes, SourceProductionContext context)
    {
        if (classes.IsDefaultOrEmpty)
        {
            // nothing to do yet
            return;
        }
        //var result = "";
        //context.AddSource("LoggerMessage.g.cs", SourceText.From(result, Encoding.UTF8));
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

