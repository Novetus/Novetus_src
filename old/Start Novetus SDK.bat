@echo off
setlocal
cd /d %~dp0
start "" "%CD%/bin/Novetus.exe" -sdk
exit