@ECHO OFF
::robocopy Novetus Novetus-lite /E
robocopy litefiles Novetus-lite /E
rmdir /s /q "Novetus-Lite\maps\Maps released by year"
rmdir /s /q "Novetus-Lite\clients\2006S"
rmdir /s /q "Novetus-Lite\clients\2006S-Shaders"
rmdir /s /q "Novetus-Lite\clients\2007M-Shaders"
rmdir /s /q "Novetus-Lite\clients\2009E"
rmdir /s /q "Novetus-Lite\shareddata\music\ROBLOX\OldSoundtrack"