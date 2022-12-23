param (
[Parameter(Mandatory)][string]$projectContext,
[Parameter(Mandatory)][string]$projectModels,
[Parameter(Mandatory)][string]$provider,
[Parameter(Mandatory)][string]$projectWeb,
[Parameter(Mandatory)][string]$nameContext,
[Parameter(Mandatory)][string]$connection

)
dotnet tool update --global dotnet-ef --version 7.0.1
#dotnet tool restore


#Remove-Item -LiteralPath $projectModels -Force -Recurse -ErrorAction SilentlyContinue 
#Remove-Item -LiteralPath $projectContext -Force -Recurse -ErrorAction SilentlyContinue 

Write-Host "search for csproj model in $projectModels"

$projectsFull = gci -Path $projectModels -File  -Filter *.csproj | Select-Object  Fullname | Select-Object -First 1


$project = $projectsFull.Fullname
Write-Host "found project $project "
$pathToModels = "Generated/Models/"+$nameContext
$pathToContext= "Generated/Context/"+$nameContext
Write-Host "pathToModels" $pathToModels



# https://docs.microsoft.com/ro-ro/ef/core/managing-schemas/scaffolding?tabs=dotnet-core-cli
dotnet ef dbcontext scaffold $connection --data-annotations  -p $project -s $project $provider -v -f --no-pluralize --no-onconfiguring --use-database-names  --context $nameContext  --context-namespace Generated --namespace Generated --context-dir $pathToContext --output-dir $pathToModels  --prefix-output --force --json #--no-build


Write-Host "search for csproj context in $projectContext"

$projectsFull = gci -Path $projectContext -File  -Filter *.csproj | Select-Object  Fullname | Select-Object -First 1


$project = $projectsFull.Fullname
Write-Host "found project context $project "
$pathToModels = "Generated/Models/"+$nameContext
$pathToContext= "Generated/Context/"+$nameContext
 dotnet ef dbcontext scaffold $connection --data-annotations  -p $project -s $project $provider -v -f --no-pluralize --no-onconfiguring  --use-database-names  --context $nameContext  --context-namespace Generated --namespace Generated --context-dir $pathToContext --output-dir $pathToModels  --prefix-output --force --json 


Write-Host "search for csproj webapi in $projectWeb"

$projectsFull = gci -Path $projectWeb -File  -Filter *.csproj | Select-Object  Fullname | Select-Object -First 1


$project = $projectsFull.Fullname
Write-Host "found project $project "
$pathToModels = "Controllers/Generated/"+$nameContext
$pathToContext= "Controllers/Generated/"+$nameContext

dotnet ef dbcontext scaffold $connection --data-annotations  -p $project -s $project $provider -v -f --no-pluralize --no-onconfiguring  --use-database-names  --context $nameContext  --context-namespace Generated --namespace Generated --context-dir $pathToContext --output-dir $pathToModels  --prefix-output --force --json #--no-build
