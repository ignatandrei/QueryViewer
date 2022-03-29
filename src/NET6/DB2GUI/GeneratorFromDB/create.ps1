param (
[Parameter(Mandatory)][string]$pathToContext,
[Parameter(Mandatory)][string]$pathToModels,
[Parameter(Mandatory)][string]$provider,
[Parameter(Mandatory)][string]$projectPath,
[Parameter(Mandatory)][string]$nameContext,
[Parameter(Mandatory)][string]$connection

)
#modify here and in WebAPI appsettings.json  
#$connection="Data Source=.;Initial Catalog=pubs;UId=sa;pwd=<YourStrong@Passw0rd>"
#$connection="Data Source=.;Initial Catalog=Northwind;UId=sa;pwd=<YourStrong@Passw0rd>"
#$connection="Data Source=.;Initial Catalog=test1;UId=sa1;pwd=yourStrong(!)Password"

dotnet tool restore

#$folder= $pathToModels
#$folder = Resolve-Path $folder  -ErrorAction SilentlyContinue -ErrorVariable _frperror
#if (-not($folder)) {
#        $folder = $_frperror[0].TargetObject
#}

Remove-Item -LiteralPath $pathToModels -Force -Recurse -ErrorAction SilentlyContinue 
Remove-Item -LiteralPath $pathToContext -Force -Recurse -ErrorAction SilentlyContinue 

Write-Host "search for csproj in $projectPath"

$projectsFull = gci -Path $projectPath -File  -Filter *.csproj | Select-Object  Fullname | Select-Object -First 1


$project = $projectsFull.Fullname
Write-Host "found project $project "


Write-Host "finding project with context " $folder



#https://docs.microsoft.com/ro-ro/ef/core/managing-schemas/scaffolding?tabs=dotnet-core-cli
dotnet ef dbcontext scaffold $connection --data-annotations  -p $project -s $project $provider -v -f --no-pluralize --use-database-names  --context $nameContext  --context-namespace Generated --namespace Generated --context-dir $pathToContext  --output-dir $pathToModels  --prefix-output --json

