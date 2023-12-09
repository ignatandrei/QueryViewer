var builder = DistributedApplication.CreateBuilder(args);

//var rb= builder.AddSqlServerContainer("Db2Gui");
//var db = rb.AddDatabase("TestDB");
//var apiservice = builder.AddProject<Projects.AspireSample_ApiService>("apiservice");

//builder.AddProject<Projects.AspireSample_Web>("webfrontend")
//    .WithReference(cache)
//    .WithReference(apiservice);

builder.Build().Run();

//var res = db.Resource;
//var cn = res.GetConnectionString();
//Console.WriteLine(cn);
