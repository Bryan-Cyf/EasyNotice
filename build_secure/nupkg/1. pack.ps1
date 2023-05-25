# Paths
$packFolder = (Get-Item -Path "./" -Verbose).FullName
$slnPath = Join-Path $packFolder "../../"
$srcPath = Join-Path $slnPath "src"

# List of projects
$projects = (
    "EasyNotice.Core",
    "EasyNotice.Dingtalk",
    "EasyNotice.Email",
	"EasyNotice.Feishu",
    "EasyNotice.Weixin"
)

# Rebuild solution
Set-Location $slnPath
& dotnet restore

# delete package
$existPackPath = Join-Path $packFolder ("*.nupkg")
Remove-Item $existPackPath

# Copy all nuget packages to the pack folder
foreach($project in $projects) {
    
    $projectFolder = Join-Path $srcPath $project

    # Create nuget pack
    Set-Location $projectFolder
    Get-ChildItem (Join-Path $projectFolder "bin/Release") -ErrorAction SilentlyContinue | Remove-Item -Recurse
    & dotnet msbuild /p:Configuration=Release
    & dotnet msbuild /p:Configuration=Release /t:pack

    # Copy nuget package
    $projectPackPath = Join-Path $projectFolder ("/bin/Release/" + $project + ".*.nupkg")
    Move-Item $projectPackPath $packFolder
}

# Go back to the pack folder
Set-Location $packFolder