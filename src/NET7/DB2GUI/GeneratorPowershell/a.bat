:1
powershell -NoProfile -NonInteractive -ExecutionPolicy ByPass -f "C:\Users\Surface1\Documents\GitHub\QueryViewer\src\NET7\DB2GUI\GeneratorFromDB\create.ps1"   -provider Microsoft.EntityFrameworkCore.SqlServer -projectContext ..\ExampleContext\ExampleContext.csproj -projectModels ..\ExampleModels\ExampleModels.csproj -projectWeb ..\ExampleWebAPI\ExampleWebAPI.csproj -nameContext ApplicationDBContext -connection "Data Source=.;Initial Catalog=TestData;UId=sa;pwd=<YourStrong@Passw0rd>;TrustServerCertificate=true;"
pause
goto 1