@ECHO OFF
del /S *.pdb
del /S *.log
del /S *.bak

del /s /q clients\2007E\content\scripts\CSMPBoot.lua
del /s /q clients\2007M\content\scripts\CSMPBoot.lua
del /s /q clients\2006S\content\scripts\CSMPBoot.lua
del /s /q clients\ClientScriptTester\content\scripts\CSMPBoot.lua

del /s /q clients\2006S\ReShade.ini
del /s /q clients\2006S\OPENGL32.log
del /s /q clients\2006S\opengl32.dll
del /s /q clients\2006S\DefaultPreset.ini
del /s /q clients\2006S\content\temp.rbxl

del /s /q clients\2007E\ReShade.ini
del /s /q clients\2007E\OPENGL32.log
del /s /q clients\2007E\opengl32.dll
del /s /q clients\2007E\DefaultPreset.ini
del /s /q clients\2007E\content\temp.rbxl

del /s /q clients\2007M\ReShade.ini
del /s /q clients\2007M\OPENGL32.log
del /s /q clients\2007M\opengl32.dll
del /s /q clients\2007M\DefaultPreset.ini
del /s /q clients\2007M\content\temp.rbxl

del /s /q clients\2008M\ReShade.ini
del /s /q clients\2008M\OPENGL32.log
del /s /q clients\2008M\opengl32.dll
del /s /q clients\2008M\DefaultPreset.ini
del /s /q clients\2008M\content\temp.rbxl

del /s /q clients\2009E\ReShade.ini
del /s /q clients\2009E\OPENGL32.log
del /s /q clients\2009E\opengl32.dll
del /s /q clients\2009E\DefaultPreset.ini
del /s /q clients\2009E\content\temp.rbxl

del /s /q clients\2009E-HD\ReShade.ini
del /s /q clients\2009E-HD\OPENGL32.log
del /s /q clients\2009E-HD\opengl32.dll
del /s /q clients\2009E-HD\DefaultPreset.ini
del /s /q clients\2009E-HD\content\temp.rbxl

del /s /q clients\2009L\ReShade.ini
del /s /q clients\2009L\OPENGL32.log
del /s /q clients\2009L\opengl32.dll
del /s /q clients\2009L\DefaultPreset.ini
del /s /q clients\2009L\content\temp.rbxl

del /s /q clients\2010L\ReShade.ini
del /s /q clients\2010L\OPENGL32.log
del /s /q clients\2010L\opengl32.dll
del /s /q clients\2010L\DefaultPreset.ini
del /s /q clients\2010L\content\temp.rbxl

del /s /q clients\2011E\ReShade.ini
del /s /q clients\2011E\OPENGL32.log
del /s /q clients\2011E\opengl32.dll
del /s /q clients\2011E\DefaultPreset.ini
del /s /q clients\2011E\content\temp.rbxl

del /s /q clients\2011M\ReShade.ini
del /s /q clients\2011M\OPENGL32.log
del /s /q clients\2011M\opengl32.dll
del /s /q clients\2011M\DefaultPreset.ini
del /s /q clients\2011M\content\temp.rbxl

del /s /q clients\2012M\ReShade.ini
del /s /q clients\2012M\OPENGL32.log
del /s /q clients\2012M\opengl32.dll
del /s /q clients\2012M\DefaultPreset.ini
del /s /q clients\2012M\content\temp.rbxl

del /s /q clients\ClientScriptTester\ReShade.ini
del /s /q clients\ClientScriptTester\OPENGL32.log
del /s /q clients\ClientScriptTester\opengl32.dll
del /s /q clients\ClientScriptTester\DefaultPreset.ini
del /s /q clients\ClientScriptTester\content\temp.rbxl

del /s /q config\servers.txt
del /s /q config\ports.txt
del /s /q config\saved.txt
del /s /q config\ReShade.ini
del /s /q config\config.ini
del /s /q config\config_customization.ini
del /s /q config\config.json
del /s /q config\config_customization.json
del /s /q config\initialfilelist.txt
del /s /q config\BadgeDatabase.ini

del /s /q config\clients\GlobalSettings2_2007E.xml
del /s /q config\clients\GlobalSettings_4_2009E.xml
del /s /q config\clients\GlobalSettings_4_2009E-HD.xml
del /s /q config\clients\GlobalSettings_4_2010L.xml
del /s /q config\clients\GlobalSettings_4_2011E.xml
del /s /q config\clients\GlobalSettings_4_2011M.xml
del /s /q config\clients\GlobalSettings2_2006S.xml
del /s /q config\clients\GlobalSettings4_2007M.xml
del /s /q config\clients\GlobalSettings7_2008M.xml
del /s /q config\clients\GlobalSettings_13_2012M.xml
del /s /q config\clients\GlobalSettings_4_2009L.xml

del /s /q bin\rootCert.pfx

rmdir /s /q maps\Custom
rmdir /s /q shareddata\assetcache
rmdir /s /q logs