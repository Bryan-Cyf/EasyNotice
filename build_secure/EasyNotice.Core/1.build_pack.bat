@echo **************************************************
@echo *                                                *
@echo *    Bryan Expert build script (1.0.1)             *
@echo *                                                *
@echo **************************************************


@echo Paths
set packFolder=%~dp0
set slnPath=%packFolder%..\..\
set srcPath=%slnPath%src

@echo List of projects %srcPath%
set packageName=EasyNotice.Core

@echo Rebuild solution %slnPath%
cd %slnPath%
dotnet restore

@echo Delete package "%packFolder%*
@rd /q/s %packFolder%Temp\

@echo Copy all NuGet packages to the pack folder

@echo ---- Pack %packageName% ----

cd %srcPath%\%packageName%
del /Q %srcPath%\%packageName%\bin\Release
dotnet msbuild /p:Configuration=Release
dotnet msbuild /p:Configuration=Release /t:pack

@echo ---- Copy %packageName% ----
mkdir %packFolder%Temp
move "%srcPath%\%packageName%\bin\Release\netstandard2.1\%packageName%.dll" "%packFolder%\Temp\"

@echo ---- Encrypt %packageName% ----
cd %packFolder%..
dotNET_Reactor.exe -q -file "%packFolder%Temp\%packageName%.dll" -suppressildasm 1 -obfuscation 1 -antitamp 1 ^
-stringencryption 1 -resourceencryption 1 -control_flow_obfuscation 1 -flow_level 8 ^
-targetfile "%packFolder%Temp\%packageName%.dll"

@pause