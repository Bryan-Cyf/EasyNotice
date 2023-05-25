# 定义变量
$confuserExPath = "E:\ConfuserEx\Confuser.CLI.exe"
$reactorPath = "E:\DotnetReactor\dotNET_Reactor.exe"
$nuspecFile = "F:\.NET_Project\MagpieBridge\Common\nugets.nuspec"
$nugetExePath = "F:\.NET_Project\MagpieBridge\Common\nuget.exe"

# 混淆 NuGet 包
#& $confuserExPath /path/to/confuser.xml

# 加密 NuGet 包
 & $reactorPath F:\.NET_Project\MagpieBridge\Common\src\Siyu.TaoBaoKe\bin\Release\netstandard2.1\Siyu.TaoBaoKe.dll /out /outputdir=F:\.NET_Project\MagpieBridge\Common\output

# 打包 NuGet 包
# & $nugetExePath pack $nuspecFile -OutputDirectory F:\.NET_Project\MagpieBridge\Common\output

# 结束脚本
