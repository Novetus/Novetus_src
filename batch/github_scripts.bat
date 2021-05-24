@ECHO OFF

SET basedir=%CD%\scripts

if not exist "%basedir%" mkdir "%basedir%"
if not exist "%basedir%/2006S" mkdir "%basedir%/2006S"
if not exist "%basedir%/2006S-Shaders" mkdir "%basedir%/2006S-Shaders"
if not exist "%basedir%/2007E" mkdir "%basedir%/2007E"
if not exist "%basedir%/2007E-Shaders" mkdir "%basedir%/2007E-Shaders"
if not exist "%basedir%/2007M" mkdir "%basedir%/2007M"
if not exist "%basedir%/2007M-Shaders" mkdir "%basedir%/2007M-Shaders"
if not exist "%basedir%/2008M" mkdir "%basedir%/2008M"
if not exist "%basedir%/2009E" mkdir "%basedir%/2009E"
if not exist "%basedir%/2009E-HD" mkdir "%basedir%/2009E-HD"
if not exist "%basedir%/2010L" mkdir "%basedir%/2010L"
if not exist "%basedir%/2011E" mkdir "%basedir%/2011E"
if not exist "%basedir%/2011M" mkdir "%basedir%/2011M"
if not exist "%basedir%/3DView" mkdir "%basedir%/3DView"

XCOPY Novetus\clients\2006S\content\scripts\CSMPFunctions.lua %basedir%/2006S
XCOPY Novetus\clients\2006S-Shaders\content\scripts\CSMPFunctions.lua %basedir%/2006S-Shaders
XCOPY Novetus\clients\2007E\content\scripts\CSMPFunctions.lua %basedir%/2007E
XCOPY Novetus\clients\2007E-Shaders\content\scripts\CSMPFunctions.lua %basedir%/2007E-Shaders
XCOPY Novetus\clients\2007M\content\scripts\CSMPFunctions.lua %basedir%/2007M
XCOPY Novetus\clients\2007M-Shaders\content\scripts\CSMPFunctions.lua %basedir%/2007M-Shaders
XCOPY Novetus\clients\2008M\content\scripts\CSMPFunctions.lua %basedir%/2008M
XCOPY Novetus\clients\2009E\content\scripts\CSMPFunctions.lua %basedir%/2009E
XCOPY Novetus\clients\2009E-HD\content\scripts\CSMPFunctions.lua %basedir%/2009E-HD
XCOPY Novetus\clients\2010L\content\scripts\CSMPFunctions.lua %basedir%/2010L
XCOPY Novetus\clients\2011E\content\scripts\CSMPFunctions.lua %basedir%/2011E
XCOPY Novetus\clients\2011M\content\scripts\CSMPFunctions.lua %basedir%/2011M
XCOPY Novetus\bin\preview\content\scripts\CSView.lua %basedir%/3DView
XCOPY Novetus\config\ContentProviders.xml %CD%