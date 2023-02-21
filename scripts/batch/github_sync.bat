@ECHO OFF

SET debug=0
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
if not exist "%gamescriptdir%/2009L" mkdir "%gamescriptdir%/2009L"
if not exist "%gamescriptdir%/2010L" mkdir "%gamescriptdir%/2010L"
if not exist "%gamescriptdir%/2011E" mkdir "%gamescriptdir%/2011E"
if not exist "%gamescriptdir%/2011M" mkdir "%gamescriptdir%/2011M"
if not exist "%gamescriptdir%/2012M" mkdir "%gamescriptdir%/2012M"

echo Copying client scripts...
XCOPY "%cd%\Novetus\clients\2006S\content\scripts\CSMPFunctions.lua" "%gamescriptdir%/2006S" /y
XCOPY "%cd%\Novetus\clients\2006S-Shaders\content\scripts\CSMPFunctions.lua" "%gamescriptdir%/2006S-Shaders" /y
XCOPY "%cd%\Novetus\clients\2007E\content\scripts\CSMPFunctions.lua" "%gamescriptdir%/2007E" /y
XCOPY "%cd%\Novetus\clients\2007E-Shaders\content\scripts\CSMPFunctions.lua" "%gamescriptdir%/2007E-Shaders" /y
XCOPY "%cd%\Novetus\clients\2007M\content\scripts\CSMPFunctions.lua" "%gamescriptdir%/2007M" /y
XCOPY "%cd%\Novetus\clients\2007M-Shaders\content\scripts\CSMPFunctions.lua" "%gamescriptdir%/2007M-Shaders" /y
XCOPY "%cd%\Novetus\clients\2008M\content\scripts\CSMPFunctions.lua" "%gamescriptdir%/2008M" /y
XCOPY "%cd%\Novetus\clients\2009E\content\scripts\CSMPFunctions.lua" "%gamescriptdir%/2009E" /y
XCOPY "%cd%\Novetus\clients\2009E-HD\content\scripts\CSMPFunctions.lua" "%gamescriptdir%/2009E-HD" /y
XCOPY "%cd%\Novetus\clients\2009L\content\scripts\CSMPFunctions.lua" "%gamescriptdir%/2009L" /y
XCOPY "%cd%\Novetus\clients\2010L\content\scripts\CSMPFunctions.lua" "%gamescriptdir%/2010L" /y
XCOPY "%cd%\Novetus\clients\2011E\content\scripts\CSMPFunctions.lua" "%gamescriptdir%/2011E" /y
XCOPY "%cd%\Novetus\clients\2011M\content\scripts\CSMPFunctions.lua" "%gamescriptdir%/2011M" /y
XCOPY "%cd%\Novetus\clients\2012M\content\scripts\CSMPFunctions.lua" "%gamescriptdir%/2012M" /y

echo.
echo Copying client corescripts...
echo.
echo 2011E
SET ecores=%gamescriptdir%\2011E\cores
if not exist "%ecores%" mkdir "%ecores%"
XCOPY "%cd%\Novetus\clients\2011E\content\scripts\cores\*.lua" "%ecores%" /sy

echo.
echo 2011M
SET mcores=%gamescriptdir%\2011M\cores
if not exist "%mcores%" mkdir "%mcores%"
XCOPY "%cd%\Novetus\clients\2011M\content\scripts\cores\*.lua" "%mcores%" /sy

echo.
echo 2012M
SET twelvemcores=%gamescriptdir%\2012M\cores
if not exist "%twelvemcores%" mkdir "%twelvemcores%"
XCOPY "%cd%\Novetus\clients\2012M\content\scripts\cores\*.lua" "%twelvemcores%" /sy

echo.
echo Copying client script libraries...
XCOPY "%cd%\Novetus\clients\2006S\content\fonts\libraries.rbxm" "%gamescriptdir%/2006S" /y
XCOPY "%cd%\Novetus\clients\2006S-Shaders\content\fonts\libraries.rbxm" "%gamescriptdir%/2006S-Shaders" /y
XCOPY "%cd%\Novetus\clients\2007E\content\fonts\libraries.rbxm" "%gamescriptdir%/2007E" /y
XCOPY "%cd%\Novetus\clients\2007E-Shaders\content\fonts\libraries.rbxm" "%gamescriptdir%/2007E-Shaders" /y
XCOPY "%cd%\Novetus\clients\2007M\content\fonts\libraries.rbxm" "%gamescriptdir%/2007M" /y
XCOPY "%cd%\Novetus\clients\2007M-Shaders\content\fonts\libraries.rbxm" "%gamescriptdir%/2007M-Shaders" /y
XCOPY "%cd%\Novetus\clients\2008M\content\fonts\libraries.rbxm" "%gamescriptdir%/2008M" /y
XCOPY "%cd%\Novetus\clients\2009E\content\fonts\libraries.rbxm" "%gamescriptdir%/2009E" /y
XCOPY "%cd%\Novetus\clients\2009E-HD\content\fonts\libraries.rbxm" "%gamescriptdir%/2009E-HD" /y
XCOPY "%cd%\Novetus\clients\2009L\content\fonts\libraries.rbxm" "%gamescriptdir%/2009L" /y
XCOPY "%cd%\Novetus\clients\2010L\content\fonts\libraries.rbxm" "%gamescriptdir%/2010L" /y
XCOPY "%cd%\Novetus\clients\2011E\content\fonts\libraries.rbxm" "%gamescriptdir%/2011E" /y
XCOPY "%cd%\Novetus\clients\2011M\content\fonts\libraries.rbxm" "%gamescriptdir%/2011M" /y
XCOPY "%cd%\Novetus\clients\2012M\content\fonts\libraries.rbxm" "%gamescriptdir%/2012M" /y

echo.
echo Copying default client configurations...
SET tempdir=%CD%\cfg-temp
if not exist "%tempdir%" mkdir "%tempdir%"
XCOPY Novetus\config\clients\*.xml %tempdir% /sy
del /s /q "%tempdir%\GlobalSettings2_2007E.xml"
del /s /q "%tempdir%\GlobalSettings2_2007E-Shaders.xml"
del /s /q "%tempdir%\GlobalSettings_4_2009E.xml"
del /s /q "%tempdir%\GlobalSettings_4_2009E-HD.xml"
del /s /q "%tempdir%\GlobalSettings_4_2009L.xml"
del /s /q "%tempdir%\GlobalSettings_4_2010L.xml"
del /s /q "%tempdir%\GlobalSettings_4_2011E.xml"
del /s /q "%tempdir%\GlobalSettings_4_2011M.xml"
del /s /q "%tempdir%\GlobalSettings4_2006S.xml"
del /s /q "%tempdir%\GlobalSettings4_2006S-Shaders.xml"
del /s /q "%tempdir%\GlobalSettings4_2007M.xml"
del /s /q "%tempdir%\GlobalSettings4_2007M-Shaders.xml"
del /s /q "%tempdir%\GlobalSettings7_2008M.xml"
del /s /q "%tempdir%\GlobalSettings_13_2012M.xml"

XCOPY "%tempdir%\GlobalSettings2_2007E_default.xml" "%gamescriptdir%/2007E" /y
XCOPY "%tempdir%\GlobalSettings2_2007E-Shaders_default.xml" "%gamescriptdir%/2007E-Shaders" /y
XCOPY "%tempdir%\GlobalSettings_4_2009E_default.xml" "%gamescriptdir%/2009E" /y
XCOPY "%tempdir%\GlobalSettings_4_2009E-HD_default.xml" "%gamescriptdir%/2009E-HD" /y
XCOPY "%tempdir%\GlobalSettings_4_2009L_default.xml" "%gamescriptdir%/2009L" /y
XCOPY "%tempdir%\GlobalSettings_4_2010L_default.xml" "%gamescriptdir%/2010L" /y
XCOPY "%tempdir%\GlobalSettings_4_2011E_default.xml" "%gamescriptdir%/2011E" /y
XCOPY "%tempdir%\GlobalSettings_4_2011M_default.xml" "%gamescriptdir%/2011M" /y
XCOPY "%tempdir%\GlobalSettings4_2006S_default.xml" "%gamescriptdir%/2006S" /y
XCOPY "%tempdir%\GlobalSettings4_2006S-Shaders_default.xml" "%gamescriptdir%/2006S-Shaders" /y
XCOPY "%tempdir%\GlobalSettings4_2007M_default.xml" "%gamescriptdir%/2007M" /y
XCOPY "%tempdir%\GlobalSettings4_2007M-Shaders_default.xml" "%gamescriptdir%/2007M-Shaders" /y
XCOPY "%tempdir%\GlobalSettings7_2008M_default.xml" "%gamescriptdir%/2008M" /y
XCOPY "%tempdir%\GlobalSettings_13_2012M_default.xml" "%gamescriptdir%/2012M" /y
rmdir "%tempdir%" /s /q

echo.
echo Copying launcher scripts...
SET launcherscriptdir=%basedir%\launcher
if not exist "%launcherscriptdir%" mkdir "%launcherscriptdir%"
if not exist "%launcherscriptdir%/3DView" mkdir "%launcherscriptdir%/3DView"
XCOPY "%cd%\Novetus\bin\preview\content\scripts\CSView.lua" "%launcherscriptdir%/3DView" /y

SET previewcores=%launcherscriptdir%\3DView\cores
if not exist "%previewcores%" mkdir "%previewcores%"
XCOPY "%cd%\Novetus\bin\preview\content\scripts\cores\*.lua" "%previewcores%" /sy

XCOPY "%cd%\Novetus\bin\preview\content\fonts\3DView.rbxl" "%launcherscriptdir%/3DView" /y
XCOPY "%cd%\Novetus\bin\data\Appreciation.rbxl" "%launcherscriptdir%" /y
XCOPY "%cd%\Novetus\config\ContentProviders.xml" "%launcherscriptdir%" /y
XCOPY "%cd%\Novetus\config\PartColors.xml" "%launcherscriptdir%" /y
XCOPY "%cd%\Novetus\config\splashes.txt" "%launcherscriptdir%" /y
XCOPY "%cd%\Novetus\config\splashes-special.txt" "%launcherscriptdir%" /y
XCOPY "%cd%\Novetus\config\names-special.txt" "%launcherscriptdir%" /y
XCOPY "%cd%\Novetus\config\info.ini" "%launcherscriptdir%" /y
XCOPY "%cd%\Novetus\config\FileDeleteFilter.txt" "%launcherscriptdir%" /y
XCOPY "%cd%\Novetus\config\term-list.txt" "%launcherscriptdir%" /y

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
XCOPY "%cd%\Novetus\addons\*.lua" "%addonsdir%" /y

SET addonscoredir=%addonsdir%\core
if not exist "%addonscoredir%" mkdir "%addonscoredir%"
XCOPY "%cd%\Novetus\addons\core\AddonLoader.lua" "%addonscoredir%" /y

SET extfolder=%addonsdir%\novetusexts
if not exist "%extfolder%" mkdir "%extfolder%"

SET extwebproxyfolder=%extfolder%\webproxy
if not exist "%extwebproxyfolder%" mkdir "%extwebproxyfolder%"
XCOPY "%cd%\Novetus\addons\novetusexts\webproxy\*.cs" "%extwebproxyfolder%" /sy

echo.
echo Coying additional files to GitHub folder...
if not exist "%dest%\scripts\batch" mkdir "%scriptsdir%\batch"
XCOPY "%cd%\dev_menu.bat" "%scriptsdir%\batch" /y
XCOPY "%cd%\Novetus\clean_junk.bat" "%scriptsdir%\batch" /y
XCOPY "%cd%\github_sync.bat" "%scriptsdir%\batch" /y
XCOPY "%cd%\assetfixer_gauntlet.lua" "%scriptsdir%" /y
XCOPY "%cd%\Novetus\Novetus_dependency_installer.bat" "%scriptsdir%\batch" /y
XCOPY "%cd%\Novetus\misc\documentation.txt" "%dest%" /y
XCOPY "%cd%\Novetus\misc\consolehelp.txt" "%dest%" /y
XCOPY /c "%cd%\Novetus\.itch.toml" "%dest%" /y
XCOPY "%cd%\Novetus\misc\masterserver\list.php" "%dest%" /y
XCOPY "%cd%\Novetus\misc\masterserver\delist.php" "%dest%" /y
XCOPY "%cd%\Novetus\misc\masterserver\LICENSE-MASTER-SERVER.txt" "%dest%\LICENSE-MASTER-SERVER" /y
XCOPY "%cd%\Novetus\changelog.txt" "%dest%\changelog.txt" /y
XCOPY "%cd%\Novetus\misc\LICENSE.txt" "%dest%\LICENSE" /y
XCOPY "%cd%\Novetus\misc\LICENSE-RESHADE.txt" "%dest%\LICENSE-RESHADE" /y
XCOPY "%cd%\Novetus\README-AND-CREDITS.TXT" "%dest%" /y
if %debug%==1 pause