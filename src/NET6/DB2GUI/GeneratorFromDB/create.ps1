param (
[Parameter(Mandatory)][string]$pathToContext,
[Parameter(Mandatory)][string]$pathToModels,
[Parameter(Mandatory)][string]$provider,
[Parameter(Mandatory)][string]$project
)
 
$connection="Data Source=.;Initial Catalog=tests;UId=sa1;pwd=yourStrong(!)Password"
 
dotnet tool restore

$folder= $pathToModels
$folder = Resolve-Path $folder  -ErrorAction SilentlyContinue -ErrorVariable _frperror
if (-not($folder)) {
        $folder = $_frperror[0].TargetObject
    }
Write-Host "finding project with context " $folder



#https://docs.microsoft.com/ro-ro/ef/core/managing-schemas/scaffolding?tabs=dotnet-core-cli
dotnet ef dbcontext scaffold $connection -p $project -s $project $provider -v -f --no-pluralize --use-database-names  --context ApplicationDbContext  --context-namespace Generated --namespace Generated --context-dir $pathToContext  --output-dir $pathToModels  --prefix-output --json

