cls
Remove-Item -Path .\a.zip -Force -Recurse -ErrorAction SilentlyContinue 
Remove-Item -Path .\GeneratorFromDB -Force -Recurse -ErrorAction SilentlyContinue 
Expand-Archive ./GeneratorFromDB.zip -DestinationPath GeneratorFromDB
Remove-Item -Path GeneratorFromDB\ExampleContext\context.txt -Force
Remove-Item -Path GeneratorFromDB\ExampleModels\models.txt -Force
Remove-Item -Path GeneratorFromDB\ExampleWebAPI\controller.txt -Force
Remove-Item -Path GeneratorFromDB\GeneratorFromDB\create.ps1 -Force
Copy-Item ..\DB2GUI\Templates\context.txt -Destination GeneratorFromDB\ExampleContext\context.txt   
Copy-Item ..\DB2GUI\Templates\models.txt -Destination GeneratorFromDB\ExampleModels\models.txt 
Copy-Item ..\DB2GUI\Templates\controller.txt -Destination GeneratorFromDB\ExampleWebAPI\controller.txt 
Copy-Item ..\GeneratorFromDB\create.ps1 -Destination GeneratorFromDB\GeneratorFromDB\create.ps1

Compress-Archive -DestinationPath .\a -Path GeneratorFromDB\*
Remove-Item .\GeneratorFromDB.zip
Remove-Item .\ProjectTemplates\GeneratorFromDB.zip
Copy-Item .\a.zip .\ProjectTemplates\GeneratorFromDB.zip
Move-Item .\a.zip .\GeneratorFromDB.zip