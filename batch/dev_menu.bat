@ECHO OFF
SET releaseoption=0
SET checkoption=0
SET cleanupval=0
:MENU
CLS
ECHO -----------------------------------------------
ECHO Novetus Release Utility
ECHO -----------------------------------------------
ECHO.
ECHO 1 - Release
ECHO 2 - Release Beta
ECHO 3 - Release Without Maps
ECHO 4 - Validate manifest
ECHO 5 - itch.io build status.
ECHO 6 - Push File List.
ECHO 7 - EXIT
ECHO.
SET /P M=Option:
IF %M%==1 SET releaseoption=1
IF %M%==1 GOTO CLEANUP
IF %M%==2 SET releaseoption=2
IF %M%==2 GOTO CLEANUP
IF %M%==3 SET releaseoption=3
IF %M%==3 GOTO CLEANUP
IF %M%==4 GOTO VALIDATE
IF %M%==5 GOTO STATUS
IF %M%==6 GOTO PUSHFILELISTMENU
IF %M%==7 EXIT

:PUSHFILELISTMENU
CLS
ECHO -----------------------------------------------
ECHO Push File List for:
ECHO -----------------------------------------------
ECHO.
ECHO 1 - Release
ECHO 2 - Release Beta
ECHO 3 - Release Without Maps
ECHO 4 - Back
ECHO.
SET /P M=Option:
IF %M%==1 SET checkoption=1
IF %M%==1 GOTO CLEANUP_DRY
IF %M%==2 SET checkoption=2
IF %M%==2 GOTO CLEANUP_DRY
IF %M%==3 SET checkoption=3
IF %M%==3 GOTO CLEANUP_DRY
IF %M%==4 GOTO MENU

:CLEANJUNK
del /S Novetus\*.pdb
del /S Novetus\*.log

del /s /q Novetus\clients\2007E\content\scripts\CSMPBoot.lua
del /s /q Novetus\clients\2007E-Shaders\content\scripts\CSMPBoot.lua
del /s /q Novetus\clients\2007M\content\scripts\CSMPBoot.lua
del /s /q Novetus\clients\2007M-Shaders\content\scripts\CSMPBoot.lua
del /s /q Novetus\clients\2006S\content\scripts\CSMPBoot.lua
del /s /q Novetus\clients\2006S-Shaders\content\scripts\CSMPBoot.lua

del /s /q Novetus\clients\2006S\ReShade.ini
del /s /q Novetus\clients\2006S\OPENGL32.log
del /s /q Novetus\clients\2006S\opengl32.dll
del /s /q Novetus\clients\2006S\DefaultPreset.ini
del /s /q Novetus\clients\2006S\content\temp.rbxl

del /s /q Novetus\clients\2006S-Shaders\ReShade.ini
del /s /q Novetus\clients\2006S-Shaders\OPENGL32.log
del /s /q Novetus\clients\2006S-Shaders\opengl32.dll
del /s /q Novetus\clients\2006S-Shaders\DefaultPreset.ini
del /s /q Novetus\clients\2006S-Shaders\content\temp.rbxl

del /s /q Novetus\clients\2007E\ReShade.ini
del /s /q Novetus\clients\2007E\OPENGL32.log
del /s /q Novetus\clients\2007E\opengl32.dll
del /s /q Novetus\clients\2007E\DefaultPreset.ini
del /s /q Novetus\clients\2007E\content\temp.rbxl

del /s /q Novetus\clients\2007E-Shaders\ReShade.ini
del /s /q Novetus\clients\2007E-Shaders\OPENGL32.log
del /s /q Novetus\clients\2007E-Shaders\opengl32.dll
del /s /q Novetus\clients\2007E-Shaders\DefaultPreset.ini
del /s /q Novetus\clients\2007E-Shaders\content\temp.rbxl

del /s /q Novetus\clients\2007M\ReShade.ini
del /s /q Novetus\clients\2007M\OPENGL32.log
del /s /q Novetus\clients\2007M\opengl32.dll
del /s /q Novetus\clients\2007M\DefaultPreset.ini
del /s /q Novetus\clients\2007M\content\temp.rbxl

del /s /q Novetus\clients\2007M-Shaders\ReShade.ini
del /s /q Novetus\clients\2007M-Shaders\OPENGL32.log
del /s /q Novetus\clients\2007M-Shaders\opengl32.dll
del /s /q Novetus\clients\2007M-Shaders\DefaultPreset.ini
del /s /q Novetus\clients\2007M-Shaders\content\temp.rbxl

del /s /q Novetus\clients\2008M\ReShade.ini
del /s /q Novetus\clients\2008M\OPENGL32.log
del /s /q Novetus\clients\2008M\opengl32.dll
del /s /q Novetus\clients\2008M\DefaultPreset.ini
del /s /q Novetus\clients\2008M\content\temp.rbxl

del /s /q Novetus\clients\2009E\ReShade.ini
del /s /q Novetus\clients\2009E\OPENGL32.log
del /s /q Novetus\clients\2009E\opengl32.dll
del /s /q Novetus\clients\2009E\DefaultPreset.ini
del /s /q Novetus\clients\2009E\content\temp.rbxl

del /s /q Novetus\clients\2009E-HD\ReShade.ini
del /s /q Novetus\clients\2009E-HD\OPENGL32.log
del /s /q Novetus\clients\2009E-HD\opengl32.dll
del /s /q Novetus\clients\2009E-HD\DefaultPreset.ini
del /s /q Novetus\clients\2009E-HD\content\temp.rbxl

del /s /q Novetus\clients\2009L\ReShade.ini
del /s /q Novetus\clients\2009L\OPENGL32.log
del /s /q Novetus\clients\2009L\opengl32.dll
del /s /q Novetus\clients\2009L\DefaultPreset.ini
del /s /q Novetus\clients\2009L\content\temp.rbxl

del /s /q Novetus\clients\2010L\ReShade.ini
del /s /q Novetus\clients\2010L\OPENGL32.log
del /s /q Novetus\clients\2010L\opengl32.dll
del /s /q Novetus\clients\2010L\DefaultPreset.ini
del /s /q Novetus\clients\2010L\content\temp.rbxl

del /s /q Novetus\clients\2011E\ReShade.ini
del /s /q Novetus\clients\2011E\OPENGL32.log
del /s /q Novetus\clients\2011E\opengl32.dll
del /s /q Novetus\clients\2011E\DefaultPreset.ini
del /s /q Novetus\clients\2011E\content\temp.rbxl

del /s /q Novetus\clients\2011M\ReShade.ini
del /s /q Novetus\clients\2011M\OPENGL32.log
del /s /q Novetus\clients\2011M\opengl32.dll
del /s /q Novetus\clients\2011M\DefaultPreset.ini
del /s /q Novetus\clients\2011M\content\temp.rbxl

del /s /q Novetus\clients\ClientScriptTester\ReShade.ini
del /s /q Novetus\clients\ClientScriptTester\OPENGL32.log
del /s /q Novetus\clients\ClientScriptTester\opengl32.dll
del /s /q Novetus\clients\ClientScriptTester\DefaultPreset.ini
del /s /q Novetus\clients\ClientScriptTester\content\temp.rbxl

del /s /q Novetus\config\servers.txt
del /s /q Novetus\config\ports.txt
del /s /q Novetus\config\ReShade.ini
del /s /q Novetus\config\config.ini
del /s /q Novetus\config\config_customization.ini

del /s /q Novetus\config\clients\GlobalSettings_4_2009E.xml
del /s /q Novetus\config\clients\GlobalSettings_4_2009L.xml
del /s /q Novetus\config\clients\GlobalSettings_4_2010L.xml
del /s /q Novetus\config\clients\GlobalSettings_4_2011E.xml
del /s /q Novetus\config\clients\GlobalSettings_4_2011M.xml
del /s /q Novetus\config\clients\GlobalSettings4_2006S.xml
del /s /q Novetus\config\clients\GlobalSettings4_2006S-Shaders.xml
del /s /q Novetus\config\clients\GlobalSettings4_2007M.xml
del /s /q Novetus\config\clients\GlobalSettings4_2007M-Shaders.xml
del /s /q Novetus\config\clients\GlobalSettings7_2008M.xml

rmdir /s /q Novetus\shareddata\assetcache

echo Junk files cleaned.
IF %cleanupval%==1 GOTO POSTCLEANUP
IF %cleanupval%==2 GOTO POSTCLEANUP_DRY

:CLEANUP
CLS
SET cleanupval==1
GOTO CLEANJUNK

:POSTCLEANUP
IF %releaseoption%==1 echo Press any key to push Release build
IF %releaseoption%==2 echo Press any key to push Beta build
IF %releaseoption%==3 echo Press any key to push Release build without Maps
pause
IF %releaseoption%==1 GOTO RELEASE
IF %releaseoption%==2 GOTO BETA
IF %releaseoption%==3 GOTO RELEASENOMAPS

:CLEANUP_DRY
CLS
SET cleanupval==2
GOTO CLEANJUNK

:POSTCLEANUP_DRY
IF %checkoption%==1 echo Press any key to check Release build
IF %checkoption%==2 echo Press any key to check Beta build
IF %checkoption%==3 echo Press any key to check Release build without Maps
pause
IF %checkoption%==1 GOTO RELEASE_DRY
IF %checkoption%==2 GOTO BETA_DRY
IF %checkoption%==3 GOTO RELEASENOMAPS_DRY

:RELEASE
CLS
ReleasePreparer.exe -release
butler push Novetus bitl/novetus:windows --if-changed --userversion-file releaseversion.txt
pause
del releaseversion.txt
GOTO MENU

:RELEASENOMAPS
CLS
ReleasePreparer.exe -lite
butler push Novetus-Lite bitl/novetus:windows-lite --if-changed --userversion-file releasenomapsversion.txt
pause
rmdir /s /q "Novetus-Lite"
del releasenomapsversion.txt
GOTO MENU

:BETA
CLS
ReleasePreparer.exe -snapshot
butler push Novetus bitl/novetus:windows-beta --if-changed --userversion-file betaversion.txt
pause
del betaversion.txt
GOTO MENU

:RELEASE_DRY
CLS
ReleasePreparer.exe -release
butler push Novetus bitl/novetus:windows --if-changed --userversion-file releaseversion.txt --dry-run
pause
del releaseversion.txt
GOTO MENU

:RELEASENOMAPS_DRY
CLS
ReleasePreparer.exe -lite
butler push Novetus-Lite bitl/novetus:windows-lite --if-changed --userversion-file releasenomapsversion.txt --dry-run
pause
rmdir /s /q "Novetus-Lite"
del releasenomapsversion.txt
GOTO MENU

:BETA_DRY
CLS
ReleasePreparer.exe -snapshot
butler push Novetus bitl/novetus:windows-beta --if-changed --userversion-file betaversion.txt --dry-run
pause
del betaversion.txt
GOTO MENU

:VALIDATE
CLS
butler validate Novetus
pause
GOTO MENU

:STATUS
CLS
echo RELEASE
butler status bitl/novetus:windows
echo LITE
butler status bitl/novetus:windows-lite
echo BETA
butler status bitl/novetus:windows-beta
pause
GOTO MENU