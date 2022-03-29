using System;
using System.Collections.Generic;
using System.Text;

namespace DB2GUI_RDCG;

// 
internal class Connection
{
    public string Provider { get; set; }
    public string FolderForContext { get; set; }
    public string FolderForClasses { get; set; }
    public string ProjectWithDesigner { get; set; }
    public string NameContext { get; set; }
    public string ConnectionString { get; set; }
}

internal class Root
{
    public string Version { get; set; }
    public Connection[] connections { get; set; }
}



