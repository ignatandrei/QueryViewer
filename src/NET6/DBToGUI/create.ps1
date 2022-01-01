cls
$connection= "Data Source=.;Initial Catalog=TestData;UId=sa;pwd=<YourStrong@Passw0rd>"
$provider = "Microsoft.EntityFrameworkCore.SqlServer"
dotnet tool restore
cd .\FastDBToGUIWebAPI\
#https://docs.microsoft.com/ro-ro/ef/core/managing-schemas/scaffolding?tabs=dotnet-core-cli
dotnet ef dbcontext scaffold $connection $provider  --no-pluralize --data-annotations --use-database-names  --no-onconfiguring --context ApplicationDbContext  --context-namespace FastDBToGuiContext --namespace FastDBToGuiModels --context-dir ../FastDBToGuiContext/Generated/ --output-dir ../FastDBToGuiModels/Generated/  --prefix-output --json
cd ..
 dotnet build