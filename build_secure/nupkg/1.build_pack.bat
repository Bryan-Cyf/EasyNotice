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
del /Q "%packFolder%*.dll"
del /Q "%packFolder%Temp\*.dll"

@echo Copy all NuGet packages to the pack folder
for %%P in (%projects%) do (

    @echo ---- Pack %%P ----
    cd %srcPath%\%%P
    del /Q %srcPath%\%%P\bin\Release
    dotnet msbuild /p:Configuration=Release
    dotnet msbuild /p:Configuration=Release /t:pack

    @echo ---- Copy %%P ----
    move "%srcPath%\%%P\bin\Release\netstandard2.1\%%P.dll" "%packFolder%"
)

@pause