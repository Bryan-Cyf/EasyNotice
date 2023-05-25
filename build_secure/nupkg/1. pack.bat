@echo Paths
set packFolder=%~dp0
set slnPath=%packFolder%..\..\
set srcPath=%slnPath%src

@echo List of projects %srcPath%
set projects= EasyNotice.Core EasyNotice.Dingtalk EasyNotice.Email EasyNotice.Feishu EasyNotice.Weixin

@echo Rebuild solution %slnPath%
cd %slnPath%
dotnet restore

@echo Delete package "%packFolder%*
del /Q "%packFolder%*.nupkg"

@echo Copy all NuGet packages to the pack folder
for %%P in (%projects%) do (

    @echo Create NuGet pack
    cd %srcPath%\%%P
    del /Q %srcPath%\%%P\bin\Release
    dotnet msbuild /p:Configuration=Release
    dotnet msbuild /p:Configuration=Release /t:pack

    @echo Copy NuGet package
    for %%F in ("%srcPath%\%%P\bin\Release\%%P.*.nupkg") do (
        move "%%F" "%packFolder%"
    )

)

@echo Go back to the pack folder
cd %packFolder%

setlocal enabledelayedexpansion

set "prefix=dotnet nuget push -s https://api.nuget.org/v3/index.json -k "

for %%I in (*.nupkg) do (
    set "filename=%%~nI%%~xI"
    echo !prefix! !filename! >> push.bat.tmp
)

move /y push.bat.tmp push.bat > nul

@pause
