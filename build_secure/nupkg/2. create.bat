@echo off
setlocal enabledelayedexpansion

set "prefix=dotnet nuget push -s https://api.nuget.org/v3/index.json -k "

for %%I in (*.nupkg) do (
    set "filename=%%~nI%%~xI"
    echo !prefix! !filename! >> push.bat.tmp
)

move /y push.bat.tmp push.bat > nul

del push.bat.tmp

pause
