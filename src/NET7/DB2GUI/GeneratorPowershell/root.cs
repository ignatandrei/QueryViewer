
internal class Connection
{
    public bool Enabled { get; set; }
    public string? Provider { get; set; }
    public string? ProjectForContext { get; set; }
    public string? ProjectForClasses { get; set; }
    public string? ProjectWithDesigner { get; set; }
    public string? NameContext { get; set; }
    public string? ConnectionString { get; set; }

    public string? ProjectCRA { get; set; }
}

internal class Root
{
    public string? Version { get; set; }
    public Connection[]? connections { get; set; }
}



