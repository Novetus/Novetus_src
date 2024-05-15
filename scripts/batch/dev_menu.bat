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
SET oldcd=%CD%
cd %CD%/Novetus/data
call clean_junk.bat
cd %oldcd%
echo Junk files cleaned. Updating GitHub scripts.
call github_sync.bat
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
IF %checkoption%==1 echo Press any key to check Release build
IF %checkoption%==2 echo Press any key to check Beta build
IF %checkoption%==1 GOTO RELEASE_DRY
IF %checkoption%==2 GOTO BETA_DRY

:RELEASE
CLS
ReleasePreparer.exe -release
butler push Novetus bitl/novetus:windows --if-changed --userversion-file releaseversion.txt
pause
GOTO MENU

:BETA
CLS
ReleasePreparer.exe -snapshot
butler push Novetus bitl/novetus:windows-beta --if-changed --userversion-file betaversion.txt
pause
GOTO MENU

:RELEASE_DRY
CLS
ReleasePreparer.exe -release
butler push Novetus bitl/novetus:windows --if-changed --userversion-file releaseversion.txt --dry-run
pause
GOTO MENU

:BETA_DRY
CLS
ReleasePreparer.exe -snapshot
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