namespace Generated;
[ApiController]
[Route("api/[controller]/[action]")]

public class MetaDataController
{
    [HttpGet]
    public string[] DBNames()
    {
        return AllDB.Singleton.DBNames;
    }

    [HttpPost("{dbName}")]
    public async Task<bool> EnsureCreated([FromServices] Func<string, DbContext?> fact, string dbName)
    {
        var exists = AllDB.Singleton.ExistsDB(dbName);
        if (!exists)
        {
            throw new ArgumentException("DB does not exists " + dbName);
        }

        var req = fact(dbName);
        if(req == null)
            throw new ArgumentException("service does not exists " + dbName);

        return await req.Database.EnsureCreatedAsync();
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
