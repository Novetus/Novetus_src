@ECHO OFF
:CLEAN
CLS
echo Cleaning build directory
rd /s /q "%cd%\Novetus\build"

echo Cleaning Novetus.Bootstrapper
rd /s /q "%cd%\Novetus\Novetus.Bootstrapper\bin"
rd /s /q "%cd%\Novetus\Novetus.Bootstrapper\obj"

echo Cleaning Novetus.ClientScriptTester
rd /s /q "%cd%\Novetus\Novetus.ClientScriptTester\bin"
rd /s /q "%cd%\Novetus\Novetus.ClientScriptTester\obj"

echo Cleaning Novetus.ReleasePreparer
rd /s /q "%cd%\Novetus\Novetus.ReleasePreparer\bin"
rd /s /q "%cd%\Novetus\Novetus.ReleasePreparer\obj"

echo Cleaning NovetusLauncher
rd /s /q "%cd%\Novetus\NovetusLauncher\bin"
rd /s /q "%cd%\Novetus\NovetusLauncher\obj"

echo Cleaning NovetusURI
rd /s /q "%cd%\Novetus\NovetusURI\bin"
rd /s /q "%cd%\Novetus\NovetusURI\obj"
goto MENU

:MENU
CLS
ECHO Which solution do you wish to load?
ECHO.
ECHO 1 - Novetus (.NET Framework 4.0)
ECHO 2 - Novetus.Tools (.NET Framework 4.0)
ECHO 3 - Novetus (.NET Framework 4.8.1)
ECHO 4 - Novetus.Tools (.NET Framework 4.8.1)
ECHO 5 - EXIT
ECHO.
SET /P M=Option:
IF %M%==1 start "" "%cd%\Novetus\Novetus.sln"
IF %M%==2 start "" "%cd%\Novetus\Novetus.Tools.sln"
IF %M%==3 start "" "%cd%\Novetus\Novetus.Net481.sln"
IF %M%==4 start "" "%cd%\Novetus\Novetus.Tools.Net481.sln"
IF %M%==5 EXIT