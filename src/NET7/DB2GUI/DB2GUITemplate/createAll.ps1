cls
Write-Host 'starting'
Remove-Item -Path .\a.zip -Force -Recurse -ErrorAction SilentlyContinue 
Remove-Item -Path .\GeneratorFromDBTemp -Force -Recurse -ErrorAction SilentlyContinue 
$FileLocation = "source.extension.vsixmanifest"
[xml]$xmlDoc =  Get-Content "source.extension.vsixmanifest"
Write-Host 'reading'
$xmlDoc | Format-List *
$node=$xmlDoc.PackageManifest.Metadata.Identity

$node=$node.SetAttribute("Version", "2023.3.11.1502")

$xmlDoc.OuterXml | Out-File $FileLocation
# $xmlDoc.Save(source.extension.vsixmanifest1)
New-item –ItemType "directory" GeneratorFromDBTemp
Copy-Item -Path GeneratorFromDB\* -Destination GeneratorFromDBTemp\ -Force -Recurse

push-location 
cd ..
dotnet clean
pop-location

Write-Host "copying files"
copy-item -Path ..\ExampleModels\ 		-Destination GeneratorFromDBTemp\ExampleModels\ 		-Force -Recurse
copy-item -Path ..\ExampleControllers\ 	-Destination GeneratorFromDBTemp\ExampleControllers\ 	-Force -Recurse
copy-item -Path ..\ExampleContext\	 	-Destination GeneratorFromDBTemp\ExampleContext\ 		-Force -Recurse
copy-item -Path ..\ExampleWebAPI\ 		-Destination GeneratorFromDBTemp\ExampleWebAPI\ 		-Force -Recurse

Write-Host "modify .cs files"
push-location 
cd GeneratorFromDBTemp
gci *.cs -r | % { 
	$content  = Get-Content $_.FullName 
	$newContent = $content -replace 'Example','$safeprojectname$'
	if ($content -ne $newContent) {
		Set-Content -Path  $_.FullName -Value $newContent
		Write-Host 'replacing ' $_.FullName 
		
	}
		
}
Write-Host "modify .csproj files"
gci *.csproj -r | % { 
	$content  = Get-Content $_.FullName 
	$newContent = $content -replace 'Example','$ext_safeprojectname$'
	if ($content -ne $newContent) {
		Set-Content -Path  $_.FullName -Value $newContent
		Write-Host 'replacing ' $_.FullName 
		
	}
		
}

pop-location 

Compress-Archive -DestinationPath .\a -Path GeneratorFromDBTemp\*
Remove-Item .\GeneratorFromDB.zip
Remove-Item .\ProjectTemplates\GeneratorFromDB.zip
Copy-Item .\a.zip .\ProjectTemplates\GeneratorFromDB.zip
Move-Item .\a.zip .\GeneratorFromDB.zip