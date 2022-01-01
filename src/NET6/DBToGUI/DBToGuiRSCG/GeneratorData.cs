namespace DBToGuiRSCG;

[Generator]
public class GeneratorData : /*ISourceGenerator*/ IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        Debugger.Launch();
        string s = "1";
        var dbContext = context.SyntaxProvider.CreateSyntaxProvider
            (
            predicate: (sn, ct) =>
            {
                if (sn is ClassDeclarationSyntax cds)
                {
                    var ret =cds.BaseList.Types.Any();
                    return ret;
                }
                return false;
            },
            transform: (gsc, ct) =>
            {
                var cds = gsc.Node as ClassDeclarationSyntax;
                var bt = cds.BaseList.Types;
                if (bt.Any())
                {
                    var q = bt[0];
                    return new Tuple<BaseTypeSyntax>(q);
                }
                return null;
            })
            .Where(it => it != null);
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

