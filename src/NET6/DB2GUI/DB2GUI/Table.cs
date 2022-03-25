namespace DB2GUI_RDCG;

public class Table
{
    public string Name { get; set; }
    
    public Table(Microsoft.CodeAnalysis.TypeInfo model)
    {
        Name = model.ConvertedType.Name;
        cols = new List<Column>();
        var membersModel = model.ConvertedType.GetMembers();
        foreach (var m in membersModel)
        {
            if (m is not IPropertySymbol ips)
                continue;
            if (ips.Type.Name == "ICollection")
                continue;
            var c = new Column();
            c.NameProperty = ips;
            cols.Add(c);
            var atts = m.GetAttributes();
            if (atts.Length == 0)
                continue;
            foreach (var att in atts)
            {
                var cls = att.AttributeClass as INamedTypeSymbol;
                var s = cls?.Name;
                if (s == "Key" || s == "KeyAttribute")
                {
                    c.IsKey = true;
                    continue;
                }

            }

        }
    }
    public List<Column> cols { get; set; }
    public Column[] keys
    {
        get
        {
            return cols.Where(it => it.IsKey).ToArray();
        }
    }
}

