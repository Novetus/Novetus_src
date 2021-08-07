@ECHO OFF

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
if not exist "%gamescriptdir%/3DView" mkdir "%gamescriptdir%/3DView"

SET launcherscriptdir=%basedir%\launcher
if not exist "%launcherscriptdir%" mkdir "%launcherscriptdir%"

XCOPY Novetus\clients\2006S\content\scripts\CSMPFunctions.lua %gamescriptdir%/2006S
XCOPY Novetus\clients\2006S-Shaders\content\scripts\CSMPFunctions.lua %gamescriptdir%/2006S-Shaders
XCOPY Novetus\clients\2007E\content\scripts\CSMPFunctions.lua %gamescriptdir%/2007E
XCOPY Novetus\clients\2007E-Shaders\content\scripts\CSMPFunctions.lua %gamescriptdir%/2007E-Shaders
XCOPY Novetus\clients\2007M\content\scripts\CSMPFunctions.lua %gamescriptdir%/2007M
XCOPY Novetus\clients\2007M-Shaders\content\scripts\CSMPFunctions.lua %gamescriptdir%/2007M-Shaders
XCOPY Novetus\clients\2008M\content\scripts\CSMPFunctions.lua %gamescriptdir%/2008M
XCOPY Novetus\clients\2009E\content\scripts\CSMPFunctions.lua %gamescriptdir%/2009E
XCOPY Novetus\clients\2009E-HD\content\scripts\CSMPFunctions.lua %gamescriptdir%/2009E-HD
XCOPY Novetus\clients\2010L\content\scripts\CSMPFunctions.lua %gamescriptdir%/2010L
XCOPY Novetus\clients\2011E\content\scripts\CSMPFunctions.lua %gamescriptdir%/2011E
XCOPY Novetus\clients\2011M\content\scripts\CSMPFunctions.lua %gamescriptdir%/2011M
XCOPY Novetus\bin\preview\content\scripts\CSView.lua %gamescriptdir%/3DView
XCOPY Novetus\config\ContentProviders.xml %launcherscriptdir%
XCOPY Novetus\config\splashes.txt %launcherscriptdir%
XCOPY Novetus\config\splashes-special.txt %launcherscriptdir%
XCOPY Novetus\config\names-special.txt %launcherscriptdir%