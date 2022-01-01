dotnet tool restore
while($true){
cd .\FastDBToGUI\
#https://docs.microsoft.com/ro-ro/ef/core/managing-schemas/scaffolding?tabs=dotnet-core-cli
dotnet ef dbcontext scaffold "Data Source=.;Initial Catalog=TestData;UId=sa;pwd=<YourStrong@Passw0rd>" Microsoft.EntityFrameworkCore.SqlServer --data-annotations --use-database-names --context OriginalDbContext
cd ..
pause
}