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
ECHO 3 - Validate manifest
ECHO 4 - itch.io build status.
ECHO 5 - Push File List.
ECHO 6 - EXIT
ECHO.
SET /P M=Option:
IF %M%==1 SET releaseoption=1
IF %M%==1 GOTO CLEANUP
IF %M%==2 SET releaseoption=2
IF %M%==2 GOTO CLEANUP
IF %M%==3 GOTO VALIDATE
IF %M%==4 GOTO STATUS
IF %M%==5 GOTO PUSHFILELISTMENU
IF %M%==6 EXIT

:PUSHFILELISTMENU
CLS
ECHO -----------------------------------------------
ECHO Push File List for:
ECHO -----------------------------------------------
ECHO.
ECHO 1 - Release
ECHO 2 - Release Beta
ECHO 3 - Back
ECHO.
SET /P M=Option:
IF %M%==1 SET checkoption=1
IF %M%==1 GOTO CLEANUP_DRY
IF %M%==2 SET checkoption=2
IF %M%==2 GOTO CLEANUP_DRY
IF %M%==3 GOTO MENU

:CLEANJUNK
del /s /q Novetus\clients\2007M\content\scripts\CSMPBoot.lua
del /s /q Novetus\clients\2007M-Shaders\content\scripts\CSMPBoot.lua
del /s /q Novetus\clients\2006S\content\scripts\CSMPBoot.lua
del /s /q Novetus\clients\2006S-Shaders\content\scripts\CSMPBoot.lua

del /s /q Novetus\clients\2006S\ReShade.ini
del /s /q Novetus\clients\2006S\OPENGL32.log
del /s /q Novetus\clients\2006S\opengl32.dll
del /s /q Novetus\clients\2006S\DefaultPreset.ini

del /s /q Novetus\clients\2006S-Shaders\ReShade.ini
del /s /q Novetus\clients\2006S-Shaders\OPENGL32.log
del /s /q Novetus\clients\2006S-Shaders\opengl32.dll
del /s /q Novetus\clients\2006S-Shaders\DefaultPreset.ini

del /s /q Novetus\clients\2007M\ReShade.ini
del /s /q Novetus\clients\2007M\OPENGL32.log
del /s /q Novetus\clients\2007M\opengl32.dll
del /s /q Novetus\clients\2007M\DefaultPreset.ini

del /s /q Novetus\clients\2007M-Shaders\ReShade.ini
del /s /q Novetus\clients\2007M-Shaders\OPENGL32.log
del /s /q Novetus\clients\2007M-Shaders\opengl32.dll
del /s /q Novetus\clients\2007M-Shaders\DefaultPreset.ini

del /s /q Novetus\clients\2008M\ReShade.ini
del /s /q Novetus\clients\2008M\OPENGL32.log
del /s /q Novetus\clients\2008M\opengl32.dll
del /s /q Novetus\clients\2008M\DefaultPreset.ini

del /s /q Novetus\clients\2009E\ReShade.ini
del /s /q Novetus\clients\2009E\OPENGL32.log
del /s /q Novetus\clients\2009E\opengl32.dll
del /s /q Novetus\clients\2009E\DefaultPreset.ini

del /s /q Novetus\clients\2009L\ReShade.ini
del /s /q Novetus\clients\2009L\OPENGL32.log
del /s /q Novetus\clients\2009L\opengl32.dll
del /s /q Novetus\clients\2009L\DefaultPreset.ini

del /s /q Novetus\clients\2010L\ReShade.ini
del /s /q Novetus\clients\2010L\OPENGL32.log
del /s /q Novetus\clients\2010L\opengl32.dll
del /s /q Novetus\clients\2010L\DefaultPreset.ini

del /s /q Novetus\clients\2011E\ReShade.ini
del /s /q Novetus\clients\2011E\OPENGL32.log
del /s /q Novetus\clients\2011E\opengl32.dll
del /s /q Novetus\clients\2011E\DefaultPreset.ini

del /s /q Novetus\clients\2011M\ReShade.ini
del /s /q Novetus\clients\2011M\OPENGL32.log
del /s /q Novetus\clients\2011M\opengl32.dll
del /s /q Novetus\clients\2011M\DefaultPreset.ini

del /s /q Novetus\config\servers.txt
del /s /q Novetus\config\ports.txt
del /s /q Novetus\config\ReShade.ini
del /s /q Novetus\config\config.ini
del /s /q Novetus\config\config_customization.ini
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
pause
IF %releaseoption%==1 GOTO RELEASE
IF %releaseoption%==2 GOTO BETA

:CLEANUP_DRY
CLS
SET cleanupval==2
GOTO CLEANJUNK

:POSTCLEANUP_DRY
CLS
IF %checkoption%==1 echo Press any key to check Release build
IF %checkoption%==2 echo Press any key to check Beta build
pause
IF %checkoption%==1 GOTO RELEASE_DRY
IF %checkoption%==2 GOTO BETA_DRY

:RELEASE
CLS
butler push Novetus bitl/novetus:windows --if-changed --userversion-file releaseversion.txt
pause
GOTO MENU

:BETA
CLS
butler push Novetus bitl/novetus:windows-beta --if-changed --userversion-file betaversion.txt
pause
GOTO MENU

:RELEASE_DRY
CLS
butler push Novetus bitl/novetus:windows --if-changed --userversion-file releaseversion.txt --dry-run
pause
GOTO MENU

:BETA_DRY
CLS
butler push Novetus bitl/novetus:windows-beta --if-changed --userversion-file betaversion.txt --dry-run
pause
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
echo BETA
butler status bitl/novetus:windows-beta
pause
GOTO MENU