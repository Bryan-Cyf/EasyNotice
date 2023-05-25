$prefix = 'dotnet nuget push -s https://api.nuget.org/v3/index.json -k  '

Get-ChildItem .\*.nupkg | Select-Object { $prefix + $_.Name}  | Out-File -width 1000 .\push.bat -Force

(gc .\push.bat | select -Skip 3) | sc .\push.bat
pause