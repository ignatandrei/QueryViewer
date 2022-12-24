global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Text;
global  using System.Threading.Tasks;
global using Generated;
global using Xunit;
global using Microsoft.EntityFrameworkCore;
global using System.IO;
global using GeneratorFromDB;

//[assembly: CollectionBehavior(MaxParallelThreads = 1)]
[assembly: CollectionBehavior(DisableTestParallelization = true)] 

public static class extensions
{

    
    public static DbContextOptions<TContext> GetOptions<TContext>()
        where TContext : DbContext
    {
    var connectionstring = "Data Source=.;Initial Catalog=TestData;UId=sa;pwd=<YourStrong@Passw0rd>;TrustServerCertificate=true;";

    var optionsBuilder = new DbContextOptionsBuilder<TContext>();
    optionsBuilder.UseSqlServer(connectionstring);
    return optionsBuilder.Options;

}
}
