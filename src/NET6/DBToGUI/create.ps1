$connection= "Data Source=.;Initial Catalog=TestData;UId=sa;pwd=<YourStrong@Passw0rd>"
$provider = "Microsoft.EntityFrameworkCore.SqlServer"
dotnet tool restore
cd .\FastDBToGUI\
#https://docs.microsoft.com/ro-ro/ef/core/managing-schemas/scaffolding?tabs=dotnet-core-cli
dotnet ef dbcontext scaffold $connection $provider --data-annotations --use-database-names --context OriginalDbContext --namespace FastDBToGuiModels  --output-dir ../FastDBToGuiModels
cd ..
