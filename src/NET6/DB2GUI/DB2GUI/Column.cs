namespace DB2GUI_RDCG;

public class Column
{
    
    public IPropertySymbol NameProperty { get; set; }
    public bool IsKey { get; set; }
    public string Name { get
        {
            return NameProperty.Name;
        } }
    public string TypeName
    {
        get
        {
            return (NameProperty.Type as INamedTypeSymbol)?.ToDisplayString();
        }
    }
}

