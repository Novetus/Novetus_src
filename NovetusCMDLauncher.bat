@echo off
:start
cls
ECHO Novetus CMD
ECHO.
ECHO 1 - Launch Normally
ECHO 2 - Launch in No3D mode.
ECHO.
ECHO 0 - Exit
ECHO.
ECHO.
SET /P A=Select a option:
IF "%A%" EQU "1" cls
IF "%A%" EQU "1" NovetusCMD.exe
IF "%A%" EQU "2" cls
IF "%A%" EQU "2" NovetusCMD.exe -no3d
IF "%A%" EQU "0" EXIT
EXIT