namespace DB2GUI_RDCG;

public class Column
{

    public IPropertySymbol NameProperty { get; set; }
    public bool IsKey { get; set; }
    public string Name
    {
        get
        {
            return NameProperty.Name;
        }
    }
    public bool IsNullable
    {
        get
        {
            return TheTypeName.Contains("?");
        }
    }
    public bool IsArray
    {
        get
        {
            return TheTypeName.Contains("[]");
        }
    }
    public string TheTypeName
    {
        get
        {
            return (NameProperty.Type as INamedTypeSymbol)?.ToDisplayString();
        }
    }
}

