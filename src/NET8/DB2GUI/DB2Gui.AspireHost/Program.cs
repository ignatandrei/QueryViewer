DeleteDockerContainers();

var builder = DistributedApplication.CreateBuilder(args);

var rb= builder.AddSqlServerContainer("Db2Gui", "<YourStrong@Passw0rd>",1433);

var api = builder.AddProject<Projects.ExampleWebAPI>(nameof(Projects.ExampleWebAPI))
    .WithEnvironment(ctx =>
    {
        var connectionStringName = $"ConnectionStrings__";
        var res = rb.Resource;
        var cn = res.GetConnectionString();
        ctx.EnvironmentVariables[connectionStringName + "ApplicationDBContext"] = cn + $";database=tests;";
        ctx.EnvironmentVariables[connectionStringName + "NorthwindDBContext"] = cn + $";database=northwind;";
        ctx.EnvironmentVariables[connectionStringName + "PubsDBContext"] = cn + $";database=pubs;";
    })
    ;


builder.AddWebAssemblyProject<Projects.ExampleBlazor>(nameof(Projects.ExampleBlazor), api);


builder.AddProject<Projects.GeneratorPowershell>(nameof(Projects.GeneratorPowershell))
    .WithEnvironment(c => c.EnvironmentVariables.Add("waitSeconds", "60"))
    
    ;
    
//builder.AddExecutable("notepad.exe","notepad.exe",Environment.CurrentDirectory);
var app = builder.Build();
//Expected allocated endpoints!
//await CreateData(rb);
//await app.RunAsync(); 



await Task.WhenAll(app.RunAsync(),CreateData(rb));

async Task<bool> CreateData(IResourceBuilder<SqlServerContainerResource> db)
{
    await Task.Delay(10_000);

    var res = db.Resource;
    var cn = res.GetConnectionString();
    Console.WriteLine(cn);
    if(string.IsNullOrEmpty(cn))
        return false;
    Console.WriteLine("Creating DB");
    //wait for sql server to start
    await Task.Delay(10_000);
    
    await CreateFromText(cn,"tests.sql");
    await CreateFromText(cn, "northwind.sql");
    await CreateFromText(cn, "pubs.sql");


    return true;
}
async Task<long> CreateFromText(string cn, string sqlFile)
{
    try
    {
        string db = Path.GetFileNameWithoutExtension(sqlFile);
        long nr = 0;
        await ExecuteSql(cn, $"create database {db}");
        var lines = await File.ReadAllLinesAsync(sqlFile);
        string sqlData = "";
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Trim().StartsWith("GO", StringComparison.InvariantCultureIgnoreCase))
            {
                nr += await ExecuteSql(cn + $";database={db};", sqlData);
                sqlData = "";
            }
            else
            {
                sqlData += lines[i] + "\r\n";
            }
        }
        Console.WriteLine($"{sqlFile} Created {nr} records");
        return nr;
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.GetType());
        Console.WriteLine(ex.Message);
        return 0;
    }
}
async Task<int> ExecuteSql(string cn, string sql)
{
    
    if(string.IsNullOrEmpty(sql))
        return 0;
    try
    {


        using var sqlConnection = new SqlConnection(cn);
        await sqlConnection.OpenAsync();
        await using var cmd = sqlConnection.CreateCommand();
        cmd.CommandText = sql;        
        var data= await cmd.ExecuteNonQueryAsync();        
        return data;
    }
    catch (Exception ex)
    {
        Console.WriteLine(sql);
        Console.WriteLine(ex.GetType());
        Console.WriteLine(ex.Message);
        throw;
    }
    
}

void DeleteDockerContainers()
{
    var process = new Process
    {
        StartInfo = new ProcessStartInfo
        {
            FileName = "powershell.exe",
            Arguments = $$"""
-Command "docker rm -f $(docker ps -a -q)"
""",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        }
    };
    process.Start();
    while (!process.StandardOutput.EndOfStream)
    {
        var line = process.StandardOutput.ReadLine();
        Console.WriteLine(line);
    }
}