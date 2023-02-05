namespace GeneratorFromDB;

public record AllDB
{
    private AllDB()
    {

    }
    public static AllDB Singleton = new();
    private Dictionary<string, MetaDB> data = new();
    public MetaDB[] DBs
    {
        get
        {
            return data.Values.ToArray();
        }
        set
        {
            throw new ArgumentException($"use {nameof(AddDb)}");
        }
    }
    public string[] DBNames
    {
        get { return data.Keys.ToArray(); }
        set
        {
            throw new ArgumentException($"use {nameof(AddDb)}");
        }
    }
    public void AddDb(MetaDB item)
    {
        data.Add(item.Name, item);
    }
}
public record MetaDB(string Name)
{
    private Dictionary<string, MetaTable> data = new(); 
    public MetaTable[] Tables
    {
        get
        {
            return data.Values.ToArray();
        }
        set
        {
            throw new ArgumentException($"use {nameof(AddTable)}");
        }
    }
    public string[] TableNames
    {
        get { return data.Keys.ToArray();}
        set
        {
            throw new ArgumentException($"use {nameof(AddTable)}");
        }
    }
    public void AddTable(MetaTable table)
    {
        data.Add(table.Name, table);
    }
}
public record MetaTable(string Name)
{
    private Dictionary<string, MetaColumn> data = new();
    public MetaColumn[] Columns
    {
        get
        {
            return data.Values.ToArray();
        }
        set
        {
            throw new ArgumentException($"use {nameof(AddColumn)}");
        }
    }
    public string[] ColumnsNames
    {
        get { return data.Keys.ToArray(); }
        set
        {
            throw new ArgumentException($"use {nameof(AddColumn)}");
        }
    }
    public void AddColumn(MetaColumn item)
    {
        data.Add(item.Name, item);
    }
}
public record MetaColumn(string Name, string Type, bool IsNullable)
{
};
