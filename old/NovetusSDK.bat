@echo off
:start
cls
ECHO Novetus SDK
ECHO.
ECHO 1 - Client SDK
ECHO 2 - ClientScript Documentation
ECHO 3 - Item SDK
ECHO.
ECHO 0 - Exit
ECHO.
ECHO.
SET /P A=Select a option:
IF "%A%" EQU "1" start Novetus.exe -clientinfo
IF "%A%" EQU "1" cls
IF "%A%" EQU "1" goto start
IF "%A%" EQU "2" start Novetus.exe -documentation
IF "%A%" EQU "2" cls
IF "%A%" EQU "2" goto start
IF "%A%" EQU "3" start Novetus.exe -itemmaker
IF "%A%" EQU "3" cls
IF "%A%" EQU "3" goto start
IF "%A%" EQU "0" EXIT
EXIT