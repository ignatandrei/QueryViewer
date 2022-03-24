namespace DB2GUI_RDCG;

internal class DBGeneratorSN : ISyntaxReceiver
{
    internal List<ClassDeclarationSyntax> models=new List<ClassDeclarationSyntax>();
    internal List<ClassDeclarationSyntax> DbContextProps = new List<ClassDeclarationSyntax>();
    internal List<TypeSyntax> dbcontext=new List<TypeSyntax>();
    public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    {
        
        {
            
            if (syntaxNode is GenericNameSyntax gns && syntaxNode.ToFullString().Contains("AddDbContext"))
            {
                //if (syntaxNode is MemberAccessExpressionSyntax maes)
                //{
                //    var x = maes.Name;
                //}
                //if (syntaxNode is GenericNameSyntax gns)
                {
                    
                    var argContext=gns.TypeArgumentList.Arguments.First();
                    dbcontext.Add(argContext);

                }
            }
        }
        {
            if (syntaxNode is ClassDeclarationSyntax cds)
            {
                if (syntaxNode.Parent is BaseNamespaceDeclarationSyntax ns)
                {
                    if (ns.Name is IdentifierNameSyntax ins)
                    {
                        if (ins.Identifier.Text == "Generated")
                            models.Add(cds);
                    }
                }
            }
        }
        {
            if (syntaxNode is PropertyDeclarationSyntax pds)
            {
                
                if (pds.Parent is ClassDeclarationSyntax cds && cds.BaseList?.Types != null && cds.BaseList.Types.Any())
                {
                    var bt = (pds.Parent as ClassDeclarationSyntax).BaseList.Types;
                    if (bt.Any())
                    {
                        foreach (var item in bt)
                        {

                            if (item.Type is not IdentifierNameSyntax i)
                                continue;

                            if (i.Identifier.ValueText == "DbContext")
                            {
                                //var cds1 = pds.Parent as ClassDeclarationSyntax;
                                if(! DbContextProps.Contains(cds))
                                    DbContextProps.Add(cds);
                            }
                                
                        }

                        
                    }
                }
            }
        }
    }
}
