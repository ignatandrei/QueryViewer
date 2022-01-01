namespace DBToGuiRSCG;

[Generator(LanguageNames.CSharp)]
public class GeneratorData : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var dbContext = context.SyntaxProvider.CreateSyntaxProvider
            (
            predicate: (sn, ct) =>
            {
                if (sn is ClassDeclarationSyntax cds)
                {
                    return cds.BaseList.Types.Any();
                }
                return false;
            },
            transform: (gsc, ct) =>
            {
                var cds=gsc.Node  as ClassDeclarationSyntax;
                var bt = cds.BaseList.Types;
                if (bt.Any())
                    return new Tuple<int>(0);
                return null;
            })
            .Where(it=>it != null);
    }
}

