@ECHO OFF
del /S *.pdb
del /S *.log
del /S *.bak

del /s /q config\servers.txt
del /s /q config\ports.txt
del /s /q config\saved.txt
del /s /q config\config.ini
del /s /q config\config_customization.ini
del /s /q config\config.json
del /s /q config\config_customization.json
del /s /q config\initialfilelist.txt
del /s /q config\BadgeDatabase.ini

del /s /q config\clients\GlobalSettings2_2007E.xml
del /s /q config\clients\GlobalSettings_4_2009E.xml
del /s /q config\clients\GlobalSettings_4_2009E-HD.xml
del /s /q config\clients\GlobalSettings_4_2010L.xml
del /s /q config\clients\GlobalSettings_4_2011E.xml
del /s /q config\clients\GlobalSettings_4_2011M.xml
del /s /q config\clients\GlobalSettings2_2006S.xml
del /s /q config\clients\GlobalSettings4_2007M.xml
del /s /q config\clients\GlobalSettings7_2008M.xml
del /s /q config\clients\GlobalSettings_13_2012M.xml
del /s /q config\clients\GlobalSettings_4_2009L.xml

del /s /q bin\rootCert.pfx

rmdir /s /q maps\Custom
rmdir /s /q shareddata\assetcache
rmdir /s /q logs