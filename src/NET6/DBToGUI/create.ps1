$pathToConnectionStringInAppSettings = "../FastDBToGuiWebAPI/appSettings.json"
$pathToWebAPI = "../FastDBToGUIWebAPI" 
$pathToContext="../FastDBToGuiContext/Generated/"
$pathToModels="../FastDBToGuiModels/Generated/"
cls
$appSettings = Get-Content -Raw -Path $pathToConnectionStringInAppSettings | ConvertFrom-Json

Write-host "initiate for " $appSettings.ConnectionStrings.DefaultConnection


#$connection= "Data Source=.;Initial Catalog=TestData;UId=sa;pwd=<YourStrong@Passw0rd>"
$connection=$appSettings.ConnectionStrings.DefaultConnection
$provider = "Microsoft.EntityFrameworkCore.SqlServer"

dotnet tool restore
cd $pathToWebAPI
#https://docs.microsoft.com/ro-ro/ef/core/managing-schemas/scaffolding?tabs=dotnet-core-cli
dotnet ef dbcontext scaffold $connection $provider -v -f --no-pluralize --use-database-names  --no-onconfiguring --context ApplicationDbContext  --context-namespace Generated --namespace Generated --context-dir $pathToContext  --output-dir $pathToModels  --prefix-output --json
#cd ..
#dotnet dotnet-cs2ts FastDBToGuiModels\Generated -o DBToGuiAng\src\Models
dotnet build

