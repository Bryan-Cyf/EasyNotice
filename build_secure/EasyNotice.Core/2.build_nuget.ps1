$packName = "EasyNotice.Core"
$nuspecTemplate = "nugets.nuspec"
$nugetPath = "../nuget.exe"
$progetUrl = "http://nuget"
$progetKey = "key"

$xml = [xml](Get-Content $nuspecTemplate)
$version = [string](Read-Host "Version:" $xml.package.metadata.version ",Input New Version") 

$xml.package.metadata.id = $packName
$xml.package.metadata.description = "test"
$xml.package.metadata.version = $version
$xml.Save($nuspecTemplate)

& $nugetPath pack $nuspecTemplate -OutputDirectory Temp

Read-Host -Prompt "Press Enter To Continue Push Nuget"

$nugetPackage = Get-ChildItem Temp/*.nupkg
$nugetPackage | ForEach-Object {
    & $nugetPath push $_.FullName -Source $progetUrl -ApiKey $progetKey
}

Read-Host -Prompt "Press Enter To Continue"
