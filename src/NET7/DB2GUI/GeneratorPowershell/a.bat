:1
rem powershell.exe -NoProfile -NonInteractive -ExecutionPolicy ByPass -f "C:\all\QueryViewer\src\NET7\DB2GUI\GeneratorPowershell\create.ps1"   -provider Microsoft.EntityFrameworkCore.SqlServer -projectContext ..\ExampleContext\ExampleContext.csproj -projectModels ..\ExampleModels\ExampleModels.csproj -projectWeb ..\ExampleControllers\ExampleControllers.csproj -nameContext ApplicationDBContext -connection "Data Source=.;Initial Catalog=TestData;UId=sa;pwd=<YourStrong@Passw0rd>;TrustServerCertificate=true;"
powershell.exe -NoProfile -NonInteractive -ExecutionPolicy ByPass -f "C:\all\QueryViewer\src\NET7\DB2GUI\GeneratorPowershell\create.ps1"   -provider Microsoft.EntityFrameworkCore.SqlServer -projectContext ..\ExampleContext\ExampleContext.csproj -projectModels ..\ExampleModels\ExampleModels.csproj -projectWeb ..\ExampleControllers\ExampleControllers.csproj -nameContext ApplicationDBContext -connection "Data Source=sqlservertestdbep.database.windows.net;Initial Catalog=tests;UId=myadmin;pwd=p@rola123;TrustServerCertificate=true;" -ProjectCRA "..\GeneratorCRA\GeneratorCRA.csproj"
rem powershell.exe -NoProfile -NonInteractive -ExecutionPolicy ByPass -f "C:\all\QueryViewer\src\NET7\DB2GUI\GeneratorPowershell\create.ps1"   -provider Microsoft.EntityFrameworkCore.SqlServer -projectContext ..\ExampleContext\ExampleContext.csproj -projectModels ..\ExampleModels\ExampleModels.csproj -projectWeb ..\ExampleControllers\ExampleControllers.csproj -nameContext ApplicationDBContext -connection "Data Source=sqlservertestdbep.database.windows.net;Initial Catalog=tests;UId=myadmin;pwd=p@rola123;TrustServerCertificate=true;" -ProjectCRA "..\GeneratorCRA\GeneratorCRA.csproj"
pause
goto 1