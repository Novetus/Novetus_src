@ECHO OFF

SET debug=1
SET basedir=%CD%\scripts

SET gamescriptdir=%basedir%\game
if not exist "%gamescriptdir%" mkdir "%gamescriptdir%"
if not exist "%gamescriptdir%/2006S" mkdir "%gamescriptdir%/2006S"
if not exist "%gamescriptdir%/2006S-Shaders" mkdir "%gamescriptdir%/2006S-Shaders"
if not exist "%gamescriptdir%/2007E" mkdir "%gamescriptdir%/2007E"
if not exist "%gamescriptdir%/2007E-Shaders" mkdir "%gamescriptdir%/2007E-Shaders"
if not exist "%gamescriptdir%/2007M" mkdir "%gamescriptdir%/2007M"
if not exist "%gamescriptdir%/2007M-Shaders" mkdir "%gamescriptdir%/2007M-Shaders"
if not exist "%gamescriptdir%/2008M" mkdir "%gamescriptdir%/2008M"
if not exist "%gamescriptdir%/2009E" mkdir "%gamescriptdir%/2009E"
if not exist "%gamescriptdir%/2009E-HD" mkdir "%gamescriptdir%/2009E-HD"
if not exist "%gamescriptdir%/2010L" mkdir "%gamescriptdir%/2010L"
if not exist "%gamescriptdir%/2011E" mkdir "%gamescriptdir%/2011E"
if not exist "%gamescriptdir%/2011M" mkdir "%gamescriptdir%/2011M"

echo Copying game scripts...
XCOPY "%cd%\Novetus\clients\2006S\content\scripts\CSMPFunctions.lua" "%gamescriptdir%/2006S"
XCOPY "%cd%\Novetus\clients\2006S-Shaders\content\scripts\CSMPFunctions.lua" "%gamescriptdir%/2006S-Shaders"
XCOPY "%cd%\Novetus\clients\2007E\content\scripts\CSMPFunctions.lua" "%gamescriptdir%/2007E"
XCOPY "%cd%\Novetus\clients\2007E-Shaders\content\scripts\CSMPFunctions.lua" "%gamescriptdir%/2007E-Shaders"
XCOPY "%cd%\Novetus\clients\2007M\content\scripts\CSMPFunctions.lua" "%gamescriptdir%/2007M"
XCOPY "%cd%\Novetus\clients\2007M-Shaders\content\scripts\CSMPFunctions.lua" "%gamescriptdir%/2007M-Shaders"
XCOPY "%cd%\Novetus\clients\2008M\content\scripts\CSMPFunctions.lua" "%gamescriptdir%/2008M"
XCOPY "%cd%\Novetus\clients\2009E\content\scripts\CSMPFunctions.lua" "%gamescriptdir%/2009E"
XCOPY "%cd%\Novetus\clients\2009E-HD\content\scripts\CSMPFunctions.lua" "%gamescriptdir%/2009E-HD"
XCOPY "%cd%\Novetus\clients\2010L\content\scripts\CSMPFunctions.lua" "%gamescriptdir%/2010L"
XCOPY "%cd%\Novetus\clients\2011E\content\scripts\CSMPFunctions.lua" "%gamescriptdir%/2011E"
XCOPY "%cd%\Novetus\clients\2011M\content\scripts\CSMPFunctions.lua" "%gamescriptdir%/2011M"

SET launcherscriptdir=%basedir%\launcher
if not exist "%launcherscriptdir%" mkdir "%launcherscriptdir%"
if not exist "%launcherscriptdir%/3DView" mkdir "%launcherscriptdir%/3DView"

echo.
echo Copying launcher scripts...
XCOPY "%cd%\Novetus\bin\preview\content\scripts\CSView.lua" "%launcherscriptdir%/3DView"
XCOPY "%cd%\Novetus\config\ContentProviders.xml" "%launcherscriptdir%"
XCOPY "%cd%\Novetus\config\splashes.txt" "%launcherscriptdir%"
XCOPY "%cd%\Novetus\config\splashes-special.txt" "%launcherscriptdir%"
XCOPY "%cd%\Novetus\config\names-special.txt" "%launcherscriptdir%"

echo.
echo Moving scripts to GitHub folder...
SET dest=G:\Projects\GitHub\Novetus\Novetus_src
SET scriptsdir=%dest%\scripts
if not exist "%scriptsdir%" mkdir "%scriptsdir%"
XCOPY /y /E "%basedir%" "%scriptsdir%"
rmdir "%basedir%" /s /q

echo.
echo Coying additional files to GitHub folder...
if not exist "%dest%\scripts\batch" mkdir "%scriptsdir%\batch"
XCOPY /y "%cd%\dev_menu.bat"  "%scriptsdir%\batch"
XCOPY /y "%cd%\github_sync.bat"  "%scriptsdir%\batch"
XCOPY /y "%cd%\Novetus\changelog.txt" "%dest%"
XCOPY /y "%cd%\Novetus\documentation.txt" "%dest%"
XCOPY /y /c "%cd%\Novetus\.itch.toml" "%dest%"
XCOPY /y "%cd%\Novetus\query.php" "%dest%"
XCOPY /y "%cd%\Novetus\LICENSE.txt" "%dest%\LICENSE"
XCOPY /y "%cd%\Novetus\LICENSE-QUERY-PHP.txt" "%dest%\LICENSE-QUERY-PHP"
XCOPY /y "%cd%\Novetus\README-AND-CREDITS.TXT" "%dest%"
if %debug%==1 pause