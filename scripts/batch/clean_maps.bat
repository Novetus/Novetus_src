@ECHO OFF
echo WARNING:
echo By running this file, you will be deleting every RBXL and RBXLX file installed in Novetus, except for maps stored in custom directories.
echo If you stored maps in the following directories, THEY WILL BE DELETED AND THEY CANNOT BE RECOVERED.
echo --------------------------------------------------------------------
echo maps\
echo maps\2006\
echo maps\2007\
echo maps\2008\
echo maps\2009\
echo maps\2010\
echo maps\2011\
echo maps\2012\
echo maps\2013\
echo maps\2014\
echo maps\2015\
echo maps\2016\
echo maps\2017\
echo maps\Novetus Exclusives\
echo --------------------------------------------------------------------
echo Press any key to begin deleting files, or close this batch file to cancel.
pause

del /s maps\*.rbxl
del /s /q maps\2006\*.rbxl
del /s /q maps\2007\*.rbxl
del /s /q maps\2008\*.rbxl
del /s /q maps\2009\*.rbxl
del /s /q maps\2010\*.rbxl
del /s /q maps\2011\*.rbxl
del /s /q maps\2012\*.rbxl
del /s /q maps\2013\*.rbxl
del /s /q maps\2014\*.rbxl
del /s /q maps\2015\*.rbxl
del /s /q maps\2016\*.rbxl
del /s /q maps\2017\*.rbxl
del /s /q "maps\Novetus Exclusives\*.rbxl"

del /s maps\*.rbxlx
del /s /q maps\2006\*.rbxlx
del /s /q maps\2007\*.rbxlx
del /s /q maps\2008\*.rbxlx
del /s /q maps\2009\*.rbxlx
del /s /q maps\2010\*.rbxlx
del /s /q maps\2011\*.rbxlx
del /s /q maps\2012\*.rbxlx
del /s /q maps\2013\*.rbxlx
del /s /q maps\2014\*.rbxlx
del /s /q maps\2015\*.rbxlx
del /s /q maps\2016\*.rbxlx
del /s /q maps\2017\*.rbxlx
del /s /q "maps\Novetus Exclusives\*.rbxlx"
pause