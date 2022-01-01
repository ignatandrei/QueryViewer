$connection= "Data Source=.;Initial Catalog=TestData;UId=sa;pwd=<YourStrong@Passw0rd>"
$provider = "Microsoft.EntityFrameworkCore.SqlServer"
dotnet tool restore
cd .\FastDBToGUI\
#https://docs.microsoft.com/ro-ro/ef/core/managing-schemas/scaffolding?tabs=dotnet-core-cli
dotnet ef dbcontext scaffold $connection $provider --data-annotations --use-database-names --context OriginalDbContext --context-namespace FastDBToGuiContext --namespace FastDBToGuiModels --context-dir . --output-dir ../FastDBToGuiModels
cd ..
