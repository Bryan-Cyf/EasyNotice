@echo **************************************************
@echo *                                                *
@echo *    WSX Expert build script (1.0.1)             *
@echo *                                                *
@echo **************************************************


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
    for %%F in ("%srcPath%\%%P\bin\Release\*\%%P.*.dll") do (
        move "%%F" "%packFolder%"
    )
)

@echo ---- Start building, please waiting ----

set confuserExPath="Path\To\ConfuserEx\Confuser.CLI.exe"
set dotNetReactorPath="Path\To\dotNET Reactor\DotNetReactor.Console.exe"
set nugetPath="Path\To\NuGet\nuget.exe"
set packageId="YourPackageId"
set version="1.0.0"
set inputAssembly="Path\To\YourAssembly.dll"
set outputDirectory="Path\To\Output"
set progetFeedUrl="https://proget.example.com/nuget-feed"
set apiKey="YourProGetApiKey"

rem Step 2: Run dotNET Reactor for encryption
%dotNetReactorPath% -file "%outputDirectory%\ObfuscatedAssembly.dll" -out "%outputDirectory%\EncryptedAssembly.dll" -config "Path\To\YourConfig.xml"

rem Step 3: Create a NuGet package with the encrypted assembly
%nugetPath% pack "Path\To\YourPackage.nuspec" -Version %version% -Properties Configuration=Release -OutputDirectory "%outputDirectory%"

rem Step 4: Push the NuGet package to ProGet
%nugetPath% push "%outputDirectory%\YourPackage.%version%.nupkg" -Source "%progetFeedUrl%" -ApiKey "%apiKey%"

echo "Obfuscation, encryption, and publishing to ProGet completed."

pause


@copy /y ..\ExternPackage\PciLib\x64\wdapi_dotnet1021.dll ..\ExternPackage
@%bld_tool% WSXExpert.sln /p:Configuration=release /p:Platform=x64 /t:rebuild >> bulid_log.log
@copy /y ..\ExternPackage\PciLib\x86\wdapi_dotnet1021.dll ..\ExternPackage
::@%bld_tool% WSXExpert.sln /p:Configuration=debug /p:Platform=x86 /t:rebuild
@if "%ERRORLEVEL%" == "0" @echo ---- Compile success! ----
@if not "%ERRORLEVEL%" == "0" @goto error2

@echo ---- Create directories ----
@rd /q/s Output\x64
@md Output
@md Output\x64

@echo ---- Copy files ----
@xcopy /e ..\WSXExpert\bin\x64\Release Output\x64

@echo ---- Delete useless files ----
@del /q/s Output\x64\*.pdb

@echo ---- Encrypt "WSXExpert.exe" ----
@dotNET_Reactor -q -file "Output\x64\WSXExpert.exe" -suppressildasm 1 -obfuscation 1 -antitamp 1 ^
                   -stringencryption 1  -resourceencryption 1 -control_flow_obfuscation 1 -flow_level 8 ^
				   -targetfile "Output\Temp\WSXExpert.exe"

@echo ---- Encrypt "WSX.Common.dll" ----
@dotNET_Reactor -q -file "Output\x64\WSX.Common.dll" -suppressildasm 1 -obfuscation 1 -antitamp 1 ^
                   -stringencryption 1  -resourceencryption 1 -control_flow_obfuscation 1 -flow_level 8 ^
				   -targetfile "Output\Temp\WSX.Common.dll"
				   
@echo ---- Encrypt "WSX.Engine.dll" ----
@dotNET_Reactor -q -file "Output\x64\WSX.Engine.dll" -suppressildasm 1 -obfuscation 1 -antitamp 1 ^
                   -stringencryption 1  -resourceencryption 1 -control_flow_obfuscation 1 -flow_level 8 ^
				   -targetfile "Output\Temp\WSX.Engine.dll"
				   
@echo ---- Encrypt "WSX.Hardware.dll" ----
@dotNET_Reactor -q -file "Output\x64\WSX.Hardware.dll" -suppressildasm 1 -obfuscation 1 -antitamp 1 ^
                   -stringencryption 1  -resourceencryption 1 -control_flow_obfuscation 1 -flow_level 8 ^
				   -targetfile "Output\Temp\WSX.Hardware.dll"

@echo ---- Encrypt "WSX.PathDrawing.dll" ----
@dotNET_Reactor -q -file "Output\x64\WSX.PathDrawing.dll" -suppressildasm 0 -obfuscation 1 -antitamp 1 ^
                   -stringencryption 1  -resourceencryption 0 -control_flow_obfuscation 1 -flow_level 7 ^
				   -targetfile "Output\Temp\WSX.PathDrawing.dll"
				   
@echo ---- Encrypt "WSX.Logger.dll" ----
@dotNET_Reactor -q -file "Output\x64\WSX.Logger.dll" -suppressildasm 0 -obfuscation 1 -antitamp 1 ^
                   -stringencryption 1  -resourceencryption 0 -control_flow_obfuscation 1 -flow_level 7 ^
				   -targetfile "Output\Temp\WSX.Logger.dll"
				  
@echo ---- Encrypt "WSX.DXF.dll" ----
@dotNET_Reactor -q -file "Output\x64\WSX.DXF.dll" -suppressildasm 0 -obfuscation 1 -antitamp 1 ^
                   -stringencryption 1  -resourceencryption 0 -control_flow_obfuscation 1 -flow_level 7 ^
				   -targetfile "Output\Temp\WSX.DXF.dll"
				   
@echo ---- Update encrypted files ----
@xcopy /y/e Output\Temp Output\x64
@rd /q/s Output\Temp
@rd /q/s Output\Temp
@rd /q/s Output\Temp
@rd /q/s Output\Temp

@goto final

:error1
@echo ---- The enviroment path of VS2017 is wrong! ----
@pause
@goto final

:error2
@echo ---- Complie failure ----
@pause
@goto final

:final
::@pause