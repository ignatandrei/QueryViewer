param (
[Parameter(Mandatory)][string]$pathToContext,
[Parameter(Mandatory)][string]$pathToModels,
[Parameter(Mandatory)][string]$provider

 
 )
 
$connection="Data Source=.;Initial Catalog=tests;UId=sa;;pwd=yourStrong(!)Password"
 
dotnet tool restore

#https://docs.microsoft.com/ro-ro/ef/core/managing-schemas/scaffolding?tabs=dotnet-core-cli
dotnet ef dbcontext scaffold $connection $provider -v -f --no-build --no-pluralize --use-database-names  --no-onconfiguring --context ApplicationDbContext  --context-namespace Generated --namespace Generated --context-dir $pathToContext  --output-dir $pathToModels  --prefix-output --json

