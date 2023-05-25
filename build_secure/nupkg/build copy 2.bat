@echo **************************************************
@echo *                                                *
@echo *    WSX Expert build script (1.0.1)             *
@echo *                                                *
@echo **************************************************

dotNET_Reactor.exe -q -file "EasyNotice.Core.dll" -suppressildasm 1 -obfuscation 1 -antitamp 1 ^
                -stringencryption 1  -resourceencryption 1 -control_flow_obfuscation 1 -flow_level 8 ^
                -targetfile "\Temp\EasyNotice.Core.dll"


@pause