@echo **************************************************
@echo *                                                *
@echo *    WSX Expert build script (1.0.1)             *
@echo *                                                *
@echo **************************************************

set packFolder=%~dp0
set packageName=EasyNotice.Core

%packFolder%\dotNET_Reactor.exe -q -file "%packFolder%%packageName%.dll" -suppressildasm 1 -obfuscation 1 -antitamp 1 ^
                -stringencryption 1  -resourceencryption 1 -control_flow_obfuscation 1 -flow_level 8 ^
                -targetfile "%packFolder%Temp\%packageName%.dll"

@pause