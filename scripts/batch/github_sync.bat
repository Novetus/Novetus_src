@ECHO OFF

SET debug=0
SET basedir=%CD%\scripts

SET gamescriptdir=%basedir%\game
if not exist "%gamescriptdir%" mkdir "%gamescriptdir%"
if not exist "%gamescriptdir%/2006S" mkdir "%gamescriptdir%/2006S"
if not exist "%gamescriptdir%/2007E" mkdir "%gamescriptdir%/2007E"
if not exist "%gamescriptdir%/2007M" mkdir "%gamescriptdir%/2007M"
if not exist "%gamescriptdir%/2008M" mkdir "%gamescriptdir%/2008M"
if not exist "%gamescriptdir%/2009E" mkdir "%gamescriptdir%/2009E"
if not exist "%gamescriptdir%/2009E-HD" mkdir "%gamescriptdir%/2009E-HD"
if not exist "%gamescriptdir%/2009L" mkdir "%gamescriptdir%/2009L"
if not exist "%gamescriptdir%/2010L" mkdir "%gamescriptdir%/2010L"
if not exist "%gamescriptdir%/2011E" mkdir "%gamescriptdir%/2011E"
if not exist "%gamescriptdir%/2011M" mkdir "%gamescriptdir%/2011M"
if not exist "%gamescriptdir%/2012M" mkdir "%gamescriptdir%/2012M"

echo.
echo 2011M
SET mcores=%gamescriptdir%\2011M\cores
if not exist "%mcores%" mkdir "%mcores%"
XCOPY "%cd%\Novetus\data\clients\2011M\content\scripts\cores\*.lua" "%mcores%" /sy

echo.
echo 2012M
SET twelvemcores=%gamescriptdir%\2012M\cores
if not exist "%twelvemcores%" mkdir "%twelvemcores%"
XCOPY "%cd%\Novetus\data\clients\2012M\content\scripts\cores\*.lua" "%twelvemcores%" /sy

echo.
echo Copying client script libraries...
XCOPY "%cd%\Novetus\data\clients\2006S\content\fonts\libraries.rbxm" "%gamescriptdir%/2006S" /y
XCOPY "%cd%\Novetus\data\clients\2007E\content\fonts\libraries.rbxm" "%gamescriptdir%/2007E" /y
XCOPY "%cd%\Novetus\data\clients\2007M\content\fonts\libraries.rbxm" "%gamescriptdir%/2007M" /y
XCOPY "%cd%\Novetus\data\clients\2008M\content\fonts\libraries.rbxm" "%gamescriptdir%/2008M" /y
XCOPY "%cd%\Novetus\data\clients\2009E\content\fonts\libraries.rbxm" "%gamescriptdir%/2009E" /y
XCOPY "%cd%\Novetus\data\clients\2009E-HD\content\fonts\libraries.rbxm" "%gamescriptdir%/2009E-HD" /y
XCOPY "%cd%\Novetus\data\clients\2009L\content\fonts\libraries.rbxm" "%gamescriptdir%/2009L" /y
XCOPY "%cd%\Novetus\data\clients\2010L\content\fonts\libraries.rbxm" "%gamescriptdir%/2010L" /y
XCOPY "%cd%\Novetus\data\clients\2011E\content\fonts\libraries.rbxm" "%gamescriptdir%/2011E" /y
XCOPY "%cd%\Novetus\data\clients\2011M\content\fonts\libraries.rbxm" "%gamescriptdir%/2011M" /y
XCOPY "%cd%\Novetus\data\clients\2012M\content\fonts\libraries.rbxm" "%gamescriptdir%/2012M" /y

echo.
echo Copying default client configurations...
SET tempdir=%CD%\cfg-temp
if not exist "%tempdir%" mkdir "%tempdir%"
XCOPY Novetus\data\config\clients\*.xml %tempdir% /sy
del /s /q "%tempdir%\GlobalSettings2_2007E.xml"
del /s /q "%tempdir%\GlobalSettings_4_2009E.xml"
del /s /q "%tempdir%\GlobalSettings_4_2009E-HD.xml"
del /s /q "%tempdir%\GlobalSettings_4_2009L.xml"
del /s /q "%tempdir%\GlobalSettings_4_2010L.xml"
del /s /q "%tempdir%\GlobalSettings_4_2011E.xml"
del /s /q "%tempdir%\GlobalSettings_4_2011M.xml"
del /s /q "%tempdir%\GlobalSettings4_2006S.xml"
del /s /q "%tempdir%\GlobalSettings4_2007M.xml"
del /s /q "%tempdir%\GlobalSettings7_2008M.xml"
del /s /q "%tempdir%\GlobalSettings_13_2012M.xml"

XCOPY "%tempdir%\GlobalSettings2_2007E_default.xml" "%gamescriptdir%/2007E" /y
XCOPY "%tempdir%\GlobalSettings_4_2009E_default.xml" "%gamescriptdir%/2009E" /y
XCOPY "%tempdir%\GlobalSettings_4_2009E-HD_default.xml" "%gamescriptdir%/2009E-HD" /y
XCOPY "%tempdir%\GlobalSettings_4_2009L_default.xml" "%gamescriptdir%/2009L" /y
XCOPY "%tempdir%\GlobalSettings_4_2010L_default.xml" "%gamescriptdir%/2010L" /y
XCOPY "%tempdir%\GlobalSettings_4_2011E_default.xml" "%gamescriptdir%/2011E" /y
XCOPY "%tempdir%\GlobalSettings_4_2011M_default.xml" "%gamescriptdir%/2011M" /y
XCOPY "%tempdir%\GlobalSettings4_2006S_default.xml" "%gamescriptdir%/2006S" /y
XCOPY "%tempdir%\GlobalSettings4_2007M_default.xml" "%gamescriptdir%/2007M" /y
XCOPY "%tempdir%\GlobalSettings7_2008M_default.xml" "%gamescriptdir%/2008M" /y
XCOPY "%tempdir%\GlobalSettings_13_2012M_default.xml" "%gamescriptdir%/2012M" /y
rmdir "%tempdir%" /s /q

echo.
echo Copying launcher scripts...
SET launcherscriptdir=%basedir%\launcher
if not exist "%launcherscriptdir%" mkdir "%launcherscriptdir%"

XCOPY "%cd%\Novetus\data\config\launcherdata\3DView.rbxl" "%launcherscriptdir%" /y
XCOPY "%cd%\Novetus\data\config\launcherdata\Appreciation.rbxl" "%launcherscriptdir%" /y
XCOPY "%cd%\Novetus\data\config\ContentProviders.json" "%launcherscriptdir%" /y
XCOPY "%cd%\Novetus\data\config\PartColors.json" "%launcherscriptdir%" /y
XCOPY "%cd%\Novetus\data\config\splashes.txt" "%launcherscriptdir%" /y
XCOPY "%cd%\Novetus\data\config\splashes-special.txt" "%launcherscriptdir%" /y
XCOPY "%cd%\Novetus\data\config\names-special.txt" "%launcherscriptdir%" /y
XCOPY "%cd%\Novetus\data\config\info.json" "%launcherscriptdir%" /y
XCOPY "%cd%\Novetus\data\config\FileDeleteFilter.txt" "%launcherscriptdir%" /y
XCOPY "%cd%\Novetus\data\config\term-list.txt" "%launcherscriptdir%" /y

echo.
echo Moving client scripts, libraries, and configurations to GitHub folder...
SET dest=G:\Projects\GitHub\Novetus\Novetus_src
SET scriptsdir=%dest%\scripts
if not exist "%scriptsdir%" mkdir "%scriptsdir%"
XCOPY /E "%basedir%" "%scriptsdir%" /sy
rmdir "%basedir%" /s /q

echo.
echo Moving default addons and extensions...
SET addonsdir=%dest%\defaultaddons
if not exist "%addonsdir%" mkdir "%addonsdir%"
XCOPY "%cd%\Novetus\data\addons\*.lua" "%addonsdir%" /y

SET addonscoredir=%addonsdir%\core
if not exist "%addonscoredir%" mkdir "%addonscoredir%"
XCOPY "%cd%\Novetus\data\addons\core\AddonLoader.lua" "%addonscoredir%" /y

SET extfolder=%addonsdir%\novetusexts
if not exist "%extfolder%" mkdir "%extfolder%"

SET extwebproxyfolder=%extfolder%\webproxy
if not exist "%extwebproxyfolder%" mkdir "%extwebproxyfolder%"
XCOPY "%cd%\Novetus\data\addons\novetusexts\webproxy\*.cs" "%extwebproxyfolder%" /sy

echo.
echo Coying additional files to GitHub folder...
if not exist "%dest%\scripts\batch" mkdir "%scriptsdir%\batch"
XCOPY "%cd%\dev_menu.bat" "%scriptsdir%\batch" /y
XCOPY "%cd%\Novetus\data\clean_junk.bat" "%scriptsdir%\batch" /y
XCOPY "%cd%\github_sync.bat" "%scriptsdir%\batch" /y
XCOPY "%cd%\assetfixer_gauntlet.lua" "%scriptsdir%" /y
XCOPY "%cd%\NovetusDependencyInstaller.nsi" "%scriptsdir%" /y
XCOPY "%cd%\WMP_instructions.txt" "%scriptsdir%" /y
XCOPY "%cd%\Novetus\data\misc\documentation.txt" "%dest%" /y
XCOPY "%cd%\Novetus\data\misc\consolehelp.txt" "%dest%" /y
XCOPY /c "%cd%\Novetus\.itch.toml" "%dest%" /y
XCOPY "%cd%\Novetus\data\misc\masterserver\list.php" "%dest%" /y
XCOPY "%cd%\Novetus\data\misc\masterserver\delist.php" "%dest%" /y
XCOPY "%cd%\Novetus\data\changelog.txt" "%dest%\changelog.txt" /y
XCOPY "%cd%\Novetus\data\misc\LICENSE.txt" "%dest%\LICENSE" /y
XCOPY "%cd%\Novetus\data\README-AND-CREDITS.TXT" "%dest%" /y

if %debug%==1 pause