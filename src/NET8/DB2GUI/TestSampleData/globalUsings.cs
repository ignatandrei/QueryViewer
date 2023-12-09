global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Text;
global  using System.Threading.Tasks;
global using Xunit;
global using Microsoft.EntityFrameworkCore;
global using System.IO;
global using GeneratorFromDB;

//[assembly: CollectionBehavior(MaxParallelThreads = 1)]
[assembly: CollectionBehavior(DisableTestParallelization = true)] 

public static class Extensions
{

    public const string folder = @"D:\gth\QueryViewer\src\NET8\DB2GUI\";
    public static DbContextOptions<TContext> GetOptions<TContext>()
        where TContext : DbContext
    {
        var nameDB =  typeof(TContext).Name;
        nameDB = nameDB.ToLower().Replace("dbcontext", "");
        //var connectionstring = $"Data Source=.;Initial Catalog={nameDB};UId=sa;pwd=<YourStrong@Passw0rd>;TrustServerCertificate=true;";
        var connectionstring = @$"DataSource={folder}{nameDB}.db";
    var optionsBuilder = new DbContextOptionsBuilder<TContext>();
        //optionsBuilder.UseSqlServer(connectionstring);
        optionsBuilder.UseSqlite(connectionstring);
    return optionsBuilder.Options;

}
}
