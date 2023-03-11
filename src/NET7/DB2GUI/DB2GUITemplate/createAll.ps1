function AddFiles{
param($folder)
push-location 
cd GeneratorFromDBTemp
cd $folder
$xml = [xml]( Get-Content "MyTemplate.vstemplate")

#$node= $xml.VSTemplate.TemplateContent.Project
#$node.ParentNode.RemoveChild($node)
#$node= $xml.SelectSingleNode("//Project")


$node= $xml.VSTemplate.TemplateContent.Project
gci *.* -r -Exclude *.csproj,*.vstemplate, __TemplateIcon.ico | % { 
	$rel = Resolve-Path -Relative $_
	$rel = $rel.replace( ".\","")
	$newelement = $xml.CreateElement("ProjectItem")
	$newelement.SetAttribute("ReplaceParameters", "true")
	# $newelement.SetAttribute("TargetFileName", $rel)
	$newelement.InnerText =$rel
	$node.AppendChild($newelement)

}
$output = $xml.OuterXml -replace 'xmlns=""', ''
$output | Out-File "MyTemplate.vstemplate"
pop-location 


}

cls
$dt = $date = Get-Date # Get the current date and time
$v = $date.ToString("yyyy.MM.dd.HHmmss")

Write-Host 'starting'
Remove-Item -Path .\a.zip -Force -Recurse -ErrorAction SilentlyContinue 
Remove-Item -Path .\GeneratorFromDBTemp -Force -Recurse -ErrorAction SilentlyContinue 
$FileLocation = "source.extension.vsixmanifest"
[xml]$xmlDoc =  Get-Content "source.extension.vsixmanifest"
Write-Host 'reading vsixmanifest'
$xmlDoc | Format-List *
$node=$xmlDoc.PackageManifest.Metadata.Identity

$node=$node.SetAttribute("Version", $v)

$xmlDoc.OuterXml | Out-File $FileLocation
# $xmlDoc.Save(source.extension.vsixmanifest1)
New-item –ItemType "directory" GeneratorFromDBTemp
Copy-Item -Path GeneratorFromDB\* -Destination GeneratorFromDBTemp\ -Force -Recurse
push-location 
cd GeneratorFromDBTemp
$FileLocation = "GeneratorAll.vstemplate"
$xml = [xml]( Get-Content $FileLocation)
Write-Host 'reading template'
$xml | Format-List *
$node = $xml.VSTemplate.TemplateData
Write-Host 'this is' $node	
$node.Name  =  "DB2Code"
$node.Description = "GeneratorFromDB" + " " + $v + " See  See https://github.com/ignatandrei/queryViewer/"
$xml.OuterXml | Out-File $FileLocation




pop-location


push-location 
cd ..
dotnet clean
pop-location

Write-Host "copying files"
copy-item -Path ..\ExampleModels\* 			-Destination GeneratorFromDBTemp\ExampleModels\ 		-Force -Recurse
copy-item -Path ..\ExampleControllers\* 	-Destination GeneratorFromDBTemp\ExampleControllers\ 	-Force -Recurse
copy-item -Path ..\ExampleContext\*	 		-Destination GeneratorFromDBTemp\ExampleContext\ 		-Force -Recurse
copy-item -Path ..\ExampleWebAPI\* 			-Destination GeneratorFromDBTemp\ExampleWebAPI\ 		-Force -Recurse
copy-item -Path ..\GeneratorPowershell\* 	-Destination GeneratorFromDBTemp\GeneratorPowershell\ 	-Force -Recurse
copy-item -Path ..\GeneratorFromDB\* 		-Destination GeneratorFromDBTemp\GeneratorFromDB\ 		-Force -Recurse


Write-Host "modify .cs files"
push-location 
cd GeneratorFromDBTemp

Write-Host "delete bin and obj"
Get-ChildItem  -Directory -Recurse -Filter "bin" | Remove-Item -Recurse
Get-ChildItem  -Directory -Recurse -Filter "obj" | Remove-Item -Recurse


gci *.cs -r | % { 
	$content  = Get-Content $_.FullName 
	$newContent = $content -replace 'Example','$safeprojectname$'
	if ($content -ne $newContent) {
		Set-Content -Path  $_.FullName -Value $newContent
		# Write-Host 'replacing ' $_.FullName 
		
	}
		
}
Write-Host "modify .csproj files"
gci *.csproj -r | % { 
	$content  = Get-Content $_.FullName 
	$newContent = $content
	$newContent = $newContent -replace 'Example','$ext_safeprojectname$.'
	$newContent = $newContent -replace "..\\GeneratorFromDB\\GeneratorFromDB.csproj",'..\$ext_specifiedsolutionname$.GeneratorFromDB\$ext_specifiedsolutionname$.GeneratorFromDB.csproj'
	if ($content -ne $newContent) {
		Set-Content -Path  $_.FullName -Value $newContent
		# Write-Host 'replacing ' $_.FullName 
		
	}
		
}

pop-location 




AddFiles "ExampleModels"
AddFiles "ExampleControllers"
AddFiles "ExampleContext" 	
AddFiles "ExampleWebAPI"
AddFiles "GeneratorPowershell"
AddFiles "GeneratorFromDB"



Compress-Archive -DestinationPath .\a -Path GeneratorFromDBTemp\*
Remove-Item .\GeneratorFromDB.zip
Remove-Item .\ProjectTemplates\GeneratorFromDB.zip
Copy-Item .\a.zip .\ProjectTemplates\GeneratorFromDB.zip
Move-Item .\a.zip .\GeneratorFromDB.zip