namespace Generated;
[ApiController]
[Route("[controller]/[action]")]

public class MetaDataController
{
    [HttpGet]
    public string[] DBNames()
    {
        return AllDB.Singleton.DBNames;
    }
    [HttpGet("{dbName}")]
    public string[] TableNames(string dbName)
    {
        return AllDB.Singleton.GetDb(dbName).TableNames;
    }

    [HttpGet("{dbName}/{tableName}")]

    public MetaColumn[] Columns(string dbName, string tableName)
    {
        return AllDB.Singleton.GetDb(dbName).GetTable(tableName).Columns;
    }

}
