cls
$connection= "Data Source=.;Initial Catalog=TestData;UId=sa;pwd=<YourStrong@Passw0rd>"
$connection="ConnectionStrings:defaultConnection"
$provider = "Microsoft.EntityFrameworkCore.SqlServer"
dotnet tool restore
cd .\FastDBToGUIWebAPI\
#https://docs.microsoft.com/ro-ro/ef/core/managing-schemas/scaffolding?tabs=dotnet-core-cli
dotnet ef dbcontext scaffold Name=$connection $provider --data-annotations --use-database-names --context OriginalDbContext --context-namespace FastDBToGuiContext --namespace FastDBToGuiModels --context-dir ../FastDBToGuiContext/ --output-dir ../FastDBToGuiModels/
cd ..
