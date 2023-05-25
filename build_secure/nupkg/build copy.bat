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
set projects= EasyNotice.Core 

@echo Rebuild solution %slnPath%
cd %slnPath%
dotnet restore

@echo Delete package "%packFolder%*
del /Q "%packFolder%*.dll"
del /Q "%packFolder%\Temp\*.dll"

@echo Copy all NuGet packages to the pack folder
for %%P in (%projects%) do (

    @echo ---- Pack %%P ----
    cd %srcPath%\%%P
    del /Q %srcPath%\%%P\bin\Release
    dotnet msbuild /p:Configuration=Release
    dotnet msbuild /p:Configuration=Release /t:pack

    @echo ---- Copy %%P ----
    move "%srcPath%\%%P\bin\Release\netstandard2.1\%%P.dll" "%packFolder%"
        
    @echo ---- Encrypt %%P ----
    %packFolder%\dotNET_Reactor.exe -q -file "%%P.dll" -suppressildasm 1 -obfuscation 1 -antitamp 1 ^
                    -stringencryption 1  -resourceencryption 1 -control_flow_obfuscation 1 -flow_level 8 ^
                    -targetfile "%packFolder%\Temp\%%P.dll"
)

@pause