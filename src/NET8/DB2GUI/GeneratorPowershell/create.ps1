param (
[Parameter(Mandatory)][string]$projectContext,
[Parameter(Mandatory)][string]$projectModels,
[Parameter(Mandatory)][string]$provider,
[Parameter(Mandatory)][string]$projectWeb,
[Parameter(Mandatory)][string]$nameContext,
[Parameter(Mandatory)][string]$connection,
[Parameter(Mandatory)][string]$ProjectCRA



)
cls
Write-host "starting "
try{
# dotnet tool update --global dotnet-ef --version 8.0.0
#dotnet tool restore
}
catch{

}
dotnet tool list -g
#Remove-Item -LiteralPath $projectModels -Force -Recurse -ErrorAction SilentlyContinue 
#Remove-Item -LiteralPath $projectContext -Force -Recurse -ErrorAction SilentlyContinue 

Write-Host "search for CRA in $ProjectCRA"

$projectsFull = gci -Path $ProjectCRA -File  -Filter *.csproj | Select-Object  Fullname | Select-Object -First 1
Write-Host "found project data"
$project = $projectsFull.Fullname


$pathToModels = "../examplecrats/src/Admin/Generated/Models/"+$nameContext
$pathToContext= "../examplecrats/src/Admin/Generated/DB/"+$nameContext
Write-Host "pathToModels" $pathToModels

function Generate{

    param (
        [Parameter(Mandatory)]
        [string]$nameWhatToGenerate,
        [Parameter(Mandatory)]
        [string]$paramPathToGenerated,
       [Parameter(Mandatory)]
        [string]$paramProjectToCompile
    )

    
    Write-Output "Generate compile " $paramProjectToCompile "and output to " $paramPathToGenerated

    $pathFolder = [System.IO.Path]::GetDirectoryName($paramProjectToCompile)
    Write-Output "start copying folder templates to " $pathFolder
    $pathTemplates = [System.IO.Path]::Combine($pathFolder,"CodeTemplates")
    Write-Host "verify " $pathTemplates 
    if (Test-Path -Path $pathTemplates) {
        Write-Host "delete " $pathTemplates         
        Remove-Item -LiteralPath $pathTemplates -Force -Recurse
    }
    
    #New-item –itemtype directory $pathTemplates
    $pathTemplatesToCopy = [System.IO.Path]::Combine($pathFolder,"GeneratorTemplates",$nameWhatToGenerate,"CodeTemplates")
    Write-Host "copy from " $pathTemplatesToCopy
    Copy-Item -Path $pathTemplatesToCopy -Destination $pathFolder -Recurse

    $pathToModels = $paramPathToGenerated + "Models/"+$nameContext
    $pathToContext= $paramPathToGenerated + "DB/"+$nameContext


    Write-Host $connection
    # https://docs.microsoft.com/ro-ro/ef/core/managing-schemas/scaffolding?tabs=dotnet-core-cli
    dotnet ef dbcontext scaffold $connection --data-annotations  -p $project -s $project $provider -v -f --no-pluralize --no-onconfiguring --use-database-names  --context $nameContext  --context-namespace Generated --namespace Generated --context-dir $pathToContext --output-dir $pathToModels  --prefix-output --force --json #--no-build

    if (Test-Path -Path $pathTemplates) {
        Write-Host "delete " $pathTemplates         
        Remove-Item -LiteralPath $pathTemplates -Force -Recurse
    }

}


# Generate "Models"  "../ExampleModels/Generated/" $project

# Generate "Context"  "../ExampleContext/Generated/" $project

# Generate "Controllers"  "../ExampleControllers/Generated/" $project 

# Generate "crats"  "../examplecrats/src/Admin/Generated/" $project

# Generate "angular"  "../ExampleAngular/src/app/Admin/Generated/" $project

Generate "blazor"  "../ExampleBlazor/Pages/Generated/" $project 
return

#Write-Host "search for csproj model in $projectModels"
#
#$projectsFull = gci -Path $projectModels -File  -Filter *.csproj | Select-Object  Fullname | Select-Object -First 1
#
#
#$project = $projectsFull.Fullname
#Write-Host "found project $project "
#$pathToModels = "Generated/Models/"+$nameContext
#$pathToContext= "Generated/Context/"+$nameContext
#Write-Host "pathToModels" $pathToModels
#
#
#
## https://docs.microsoft.com/ro-ro/ef/core/managing-schemas/scaffolding?tabs=dotnet-core-cli
# dotnet ef dbcontext scaffold $connection --data-annotations  -p $project -s $project $provider -v -f --no-pluralize --no-onconfiguring --use-database-names  --context $nameContext  --context-namespace Generated --namespace Generated --context-dir $pathToContext --output-dir $pathToModels  --prefix-output --force --json #--no-build
#
#Write-Host "search for csproj context in $projectContext"
#
#$projectsFull = gci -Path $projectContext -File  -Filter *.csproj | Select-Object  Fullname | Select-Object -First 1
#
#
#$project = $projectsFull.Fullname
#Write-Host "found project context $project "
#$pathToModels = "Generated/Models/"+$nameContext
#$pathToContext= "Generated/Context/"+$nameContext
# dotnet ef dbcontext scaffold $connection --data-annotations  -p $project -s $project $provider -v -f --no-pluralize --no-onconfiguring  --use-database-names  --context $nameContext  --context-namespace Generated --namespace Generated --context-dir $pathToContext --output-dir $pathToModels  --prefix-output --force --json 
#
#
#
#Write-Host "search for csproj webapi in $projectWeb"
#
#$projectsFull = gci -Path $projectWeb -File  -Filter *.csproj | Select-Object  Fullname | Select-Object -First 1
#
#
#$project = $projectsFull.Fullname
#Write-Host "found project $project "
#$pathToModels = "Generated/"+$nameContext
#$pathToContext= "Generated/Context/"+$nameContext
#
#dotnet ef dbcontext scaffold $connection --data-annotations  -p $project -s $project $provider -v -f --no-pluralize --no-onconfiguring  --use-database-names  --context $nameContext  --context-namespace Generated --namespace Generated --context-dir $pathToContext --output-dir $pathToModels  --prefix-output --force --json #--no-build
#