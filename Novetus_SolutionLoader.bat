@ECHO OFF
:CLEAN
CLS
echo Cleaning Novetus.Bootstrapper
rd /s /q "%cd%\Novetus\Novetus.Bootstrapper\bin"
rd /s /q "%cd%\Novetus\Novetus.Bootstrapper\obj"
rd /s /q "%cd%\Novetus\Novetus.Bootstrapper\bin_net6"
rd /s /q "%cd%\Novetus\Novetus.Bootstrapper\obj_net6"

echo Cleaning Novetus.ClientScriptTester
rd /s /q "%cd%\Novetus\Novetus.ClientScriptTester\bin"
rd /s /q "%cd%\Novetus\Novetus.ClientScriptTester\obj"
rd /s /q "%cd%\Novetus\Novetus.ClientScriptTester\bin_net6"
rd /s /q "%cd%\Novetus\Novetus.ClientScriptTester\obj_net6"

echo Cleaning Novetus.ReleasePreparer
rd /s /q "%cd%\Novetus\Novetus.ReleasePreparer\bin"
rd /s /q "%cd%\Novetus\Novetus.ReleasePreparer\obj"
rd /s /q "%cd%\Novetus\Novetus.ReleasePreparer\bin_net6"
rd /s /q "%cd%\Novetus\Novetus.ReleasePreparer\obj_net6"

echo Cleaning NovetusLauncher
rd /s /q "%cd%\Novetus\NovetusLauncher\bin"
rd /s /q "%cd%\Novetus\NovetusLauncher\obj"
rd /s /q "%cd%\Novetus\NovetusLauncher\bin_net6"
rd /s /q "%cd%\Novetus\NovetusLauncher\obj_net6"

echo Cleaning NovetusURI
rd /s /q "%cd%\Novetus\NovetusURI\bin"
rd /s /q "%cd%\Novetus\NovetusURI\obj"
rd /s /q "%cd%\Novetus\NovetusURI\bin_net6"
rd /s /q "%cd%\Novetus\NovetusURI\obj_net6"
goto MENU

:MENU
CLS
ECHO Which solution do you wish to load?
ECHO.
ECHO 1 - Novetus (.NET Framework 4.0)
ECHO 2 - Novetus.Tools (.NET Framework 4.0)
ECHO 3 - Novetus (.NET 6)
ECHO 4 - Novetus.Tools (.NET 6)
ECHO 5 - EXIT
ECHO.
SET /P M=Option:
IF %M%==1 start "" "%cd%\Novetus\Novetus.sln"
IF %M%==2 start "" "%cd%\Novetus\Novetus.Tools.sln"
IF %M%==3 start "" "%cd%\Novetus\Novetus.Net6.sln"
IF %M%==4 start "" "%cd%\Novetus\Novetus.Tools.Net6.sln"
IF %M%==5 EXIT