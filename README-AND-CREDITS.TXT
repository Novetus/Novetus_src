Novetus Readme

https://bitl.itch.io/novetus
https://gamejolt.com/games/Novetus/375720
https://archive.org/details/novetus-13-archive
https://github.com/Novetus/Novetus_src
https://github.com/Novetus/Novetus_src/wiki

-------------------------------
INFORMATION
-------------------------------

[What is Novetus?]
​Novetus is a free, multi-version ROBLOX client launcher 
built to allow the user to run LAN and Internet servers.
						
Novetus was made to improve on my previous project, RBXLegacy, 
with the addition of new features that enhance the launcher for mod development, 
player customization, and overall usability.

Novetus is based on RBXLegacy 1.16.2, however it uses some features from RBXLegacy 1.18,
such as extended customization, better layout, improved networking features, avatar
previewing, and more.

Enhancements from RBXLegacy The Final Update 1.16.2 & 1.18.1:
- Fully offline customization and wider customization options
- Enhanced 3D Avatar Preview
- An expanded SDK with many tools to use.
- Improved UPnP functionality
- Ability to reset server/client port to default
- Easier to read join tab note
- Better launcher and client security
- Sharing of customized players with other players.
- ClientScript scripting language for creating more customized client command arguments
- Better client support. (from March 2007 up to the latest ROBLOX client with ClientScript!)
- Custom client warnings
- Command arguments for different functions
- Many more items
- Redesigned launcher
- Largely reworked codebase
- Custom player icons
- A command-line utility for hosting servers.
- Addon support.
- 2006 Color presets!
- More character colors.
- Graphical Options menu with Automatic and Custom options.
- ReShade integration.
- Online clothing support with customizable clothing providers.
- 2 styles; an Extended style for easy access to options, and a Compact style.
- Lite version.
- Much more stable.

--------------------------------
INSTRUCTIONS
--------------------------------

WARNING: 
If Hamachi is on, you won't be able to join "localhost" or any other local or private IP address! In order to use Novetus on LAN, 
you must either turn off Hamachi by pressing the "Turn Off" button, or by turning one of your Hamachi networks online if you own a network.

NOTE:
Make sure the server you are trying to join is in your region of the world (I.E West US, East US, UK, etc), if it is not you may experience major network lag issues.
(i.e. if you are in Arizona and someone hosts a server in Florida, you will not have a fun time because it will lag a lot.)
If you have a problem where you can't see your character, REGENERATE YOUR PLAYER ID THEN REJOIN THE SERVER.
If you tried to connect to the server and you get an error, REJOIN THE SERVER.

--------------------------------
LAUNCHER INSTRUCTIONS
--------------------------------

NOTE: If you get an error trying to open the launcher, install this: https://www.microsoft.com/en-us/download/details.aspx?id=17718
You must also have DirectX 9.0c installed and a graphics card/sound card that supports it. Everyone should have it by now.

--------------------------------
HOSTING
--------------------------------

How to host a server:

1. Port forward your specified port in the OPTIONS tab as UCP. If you do not know how to port forward, there are plenty of tutorials on the internet for this.
2. Open the Novetus Launcher.
3. Select the client you want.
4. Select the "START SERVER" tab.
5. Select the name of the map you want to play, then press the "START SERVER" button. The Studio window should load with the map you chose.
6. To share your server with friends, send them your public/external IP address (Google ip).

How to host a LAN server:

1. First, you must know your LAN IPv4 address from ipconfig. If you don't know your LAN IPv4 address, there are plenty of tutorials on the internet for this.
2. Open the Novetus Launcher.
3. Select the client you want.
4. Select the "START SERVER" tab.
5. Select the name of the map you want to play, then press the "START SERVER" button. The Studio window should load with the map you chose.
6. To share your server with friends, tell them your LAN IPv4 address.

How to host a Hamachi server:

1. Make sure you have Hamachi installed and you made a network already. If you do not know how, there are plenty of tutorials on the internet for this.
2. Open the Novetus Launcher.
3. Select the client you want.
4. Select the "START SERVER" tab.
5. Select the name of the map you want to play, then press the "START SERVER" button. The Studio window should load with the map you chose.
6. To share your server with friends, send them your Hamachi IPv4 address located above your computer's name on the Hamachi window. 
You must also share the network ID and password to your Hamachi network.

How to host a Playit.gg server:

1. Make sure you have Playit.gg installed and you set up a custom UDP tunnel for your server's port (from the OPTIONS tab) already. If you do not know how, there are plenty of tutorials on the internet for this.
2. Open the Novetus Launcher.
3. Select the client you want.
4. Select the "START SERVER" tab.
5. Select the name of the map you want to play, then press the "START SERVER" button. The Studio window should load with the map you chose.
6. To share your server with friends, send them your tunnel's address and port.

How to host a Master server:

1. Find your IPv4 IP, Hamachi IP, or Playit.gg IP, you will use this to share your server. You may need to port forward the port "80" on TCP and UDP.
2. Install a web server like WAMPServer.
3. After installing it, start up the WAMPServer application. Wait until the "W" on your taskbar goes green.
4. Left click the "W", then click on the "www directory" option. Windows Explorer will load with a folder named "www".
5. Copy the query.php from Novetus' directory over to the opened "www" folder.
6. Your master server is now up! You can now share your master server with other users.

--------------------------------
CONNECTING
--------------------------------

How to connect via IP (LAN/Online):

1. Open the Novetus Launcher.
2. Select the client the server uses.
3. Select the "JOIN SERVER" tab.
4. Type in the IP address that is shared to you into the "IP Address" box, and then press the "JOIN SERVER" button.
5. You should be able to join the server.

How to connect via Hamachi:

1. Open the Novetus Launcher.
2. Select the client the server uses.
3. Select the "JOIN SERVER" tab.
4. Type in the IPv4 Address that is in the title of the Hamachi network you are in into the "IP Address" box, and then press press the "JOIN SERVER" button.
5. You should be able to join the server.

--------------------------------
CUSTOMIZATION
--------------------------------

How to use a custom icon:

1. Open the Novetus Launcher.
2. Go to "Avatar" or "Character Customization"
3. Select the "OTHER" tab.
4. Click "Disable Icon/Enable Custom Icons" and click Browse to look for a custom icon (must be a .png file)
5. Novetus will now set the image as your icon. You should now be able to see your icon on the server, however no one would be able to see it except you. 
For other people to see it, you need to share it to them and they would need to do this same process but use your name as the file name for the png if the name was changed.

------------------------------------------------------------
KNOWN ISSUES AND SOLUTIONS:
------------------------------------------------------------

NOVETUS DOESN'T LOAD!
Install .NET Framework 4 (in the _redist folder or through the Dependency Installer)

SIDE-BY-SIDE CONFIGURATION ERROR FOR 2007!
Install Visual C++ 2005 Redistributable (in the _redist folder or through the Dependency Installer)

CAN'T MOVE WITH WASD in 2010L-2011M!
Delete everything in %localappdata%/Roblox and %appdata%/Roblox. If this doesn't work, try disabling game overlay on Discord.

2009E/2009E-HD DOESN'T LOAD IMGUR IMAGES PROPERLY!
Sadly, there's currently no solution for this. If you know a possible solution for this, let me know.

MY CLIENT DOES NOT LOAD A MAP!
Try selecting the map again and then load it.

MY CUSTOM 2007-EARLY 2008 CLIENT CRASHES OR CAN'T LOAD A MAP!
Open up your clientinfo.nov in the Client SDK, and check the "Doesn't have graphics mode options" and "Fix Scripts and Map Loading for 2007-Early 2008" options.

ALT-TABBING OUT OF A CLIENT DOESN'T LET ME USE THE TASK BAR OR ANYTHING!
Press Ctrl+Alt+Del and click on a button (i.e, cancel or task manager) a couple times.

NOVETUS CRASHES ON WINDOWS XP EVEN WITH .NET FRAMEWORK 4 INSTALLED!
Install the KB2468871 update for NET Framework 4 (in the _redist folder or through the Dependency Installer)

------------------------------------------------------------
VERSION DIFFERENCES:
------------------------------------------------------------

Lite:
+ Smaller File Size
- Less Maps
- Less Clients
- Less SDK tools
- Doesn't include soundtrack

Full:
- Larger file size
+ Full selection of Maps
+ Full selection of Clients
+ Full selection of SDK tools
+ Includes the ROBLOX soundtrack

------------------------------------------------------------
GRAPHICS MODE INFO:
------------------------------------------------------------

Automatic: The graphics mode will change depending on your hardware configuration. Forced on in some clients to increase stability. 
DirectX: Sets the renderer to DirectX in supported clients.
GL Stable: Sets the renderer to OpenGL Stable in supported clients.
GL Experimental: Sets the renderer to OpenGL Experimental in supported clients.

------------------------------------------------------------
GRAPHICS QUALITY INFO:
------------------------------------------------------------

Automatic:
Anti-Aliasing: Automatic
Bevels: Automatic
Shadows (2008 and up): Automatic
Quality Level (2010 and up): Automatic
Material Quality/Truss Detail (2009 and up): Automatic
Mesh Detail: 100%
Shading Quality: 100%
AA Samples: 8x
Shadows (2007): On

Very Low:
Anti-Aliasing: Off
Bevels: Off
Shadows (2008 and up): Off
Quality Level (2010 and up): 1
Material Quality/Truss Detail (2009 and up): Low
Mesh Detail: 50%
Shading Quality: 50%
AA Samples: None
Shadows (2007): Off

Low:
Anti-Aliasing: Off
Bevels: Off
Shadows (2008 and up): Off
Quality Level (2010 and up): 5
Material Quality/Truss Detail (2009 and up): Low
Mesh Detail: 50%
Shading Quality: 50%
AA Samples: None
Shadows (2007): Off

Medium:
Anti-Aliasing: On
Bevels: Off
Shadows (2008 and up): Character Only (or On on other clients)
Quality Level (2010 and up): 10
Material Quality/Truss Detail (2009 and up): Medium
Mesh Detail: 75%
Shading Quality: 75%
AA Samples: 4x
Shadows (2007): Off

High:
Anti-Aliasing: On
Bevels: On
Shadows (2008 and up): On
Quality Level (2010 and up): 15
Material Quality/Truss Detail (2009 and up): High
Mesh Detail: 75%
Shading Quality: 75%
AA Samples: 4x
Shadows (2007): On

Ultra:
Anti-Aliasing: On
Bevels: On
Shadows (2008 and up): On
Quality Level (2010 and up): 21 or 19 (depending on client)
Material Quality/Truss Detail (2009 and up): High
Mesh Detail: 100%
Shading Quality: 100%
AA Samples: 8x
Shadows (2007): On

Custom:
All graphics options are changeable. 

------------------------------------------------------------
CREDITS AND LICENSES:
------------------------------------------------------------

ROBLOX and the ROBLOX Clients were made by the ROBLOX Corporation.
The ROBLOX Corporation does not support or endorse the creation of Novetus.
Bitl is not affiliated with the ROBLOX Corporation or its subsidiaries.
Bitl does not own the majority of the places or items included with Novetus.
Novetus is not associated with Novetus Engineering LLC.
Novetus uses the majority of the Whimsee's Map Pack in the "full" version. Credits go to Whimsee and many other people for making that pack possible.
Thank you to everyone who has contributed a map, item, or client including cole and many other people. 
LUA scripts were used to build a client that can connect to LAN and the Internet.
The LUA scripts used were borrowed from the RBXPri client and merged into 1 single script.
All credit for the LUA code included with the RBXPri client goes to the RBXPri team.
All credit for the LUA code used with "non-modern" clients goes to Scripter John and EnergyCell.
All credit for the LUA code used for character customization goes to RBXBanLand.
Parts of the codebase use bits and pieces of code from Stack Overflow, MSDN Forums, the Novetus GitHub Pull Requests and Codeproject.
The original concept for the Diogenes editor was suggested by Carrot. The concept code was then modified to be smaller, more efficient, and more customizable.
ObjToRBXMesh was made by coke. Modified to support 1.00 and 1.01. (https://github.com/Novetus/ObjToRBXMesh)
Roblox Legacy Place Converter was made by BakonBot. (https://github.com/BakonBot/legacy-place-converter)
ROBLOX Script Generator was made by S. Costeira.
Thank you to NT_x86 for helping me with security fixes.
Thank you XlXi for the idea of the original logo. This logo was remade in newer verions in higher quality.
Thank you Nukley for the idea of the Splash Tester. 
Credits go to Nostal-ia for getting 2011M corescripts working and helping me with 2011E corescripts.
Credits to Hazelnut (creator of JRBX) for the buttons used in the Stylish style.
Credits go to davcs86 for the HWID generation code (https://github.com/davcs86/csharp-uhwid)
All credits for the used pieces of code go to the respective authors.

------------------------------------------------------------
Mark James' Silk icon set 1.3
------------------------------------------------------------

The Discord Rich Presence icons and the SDK icons used for this application use Mark James' Silk icon set 1.3.
http://www.famfamfam.com/lab/icons/silk/

------------------------------------------------------------
ReShade
------------------------------------------------------------

Copyright 2014 Patrick Mours. All rights reserved.

Redistribution and use in source and binary forms, with or without modification, 
are permitted provided that the following conditions are met:

Redistributions of source code must retain the above copyright notice, 
this list of conditions and the following disclaimer.

Redistributions in binary form must reproduce the above copyright notice, 
this list of conditions and the following disclaimer in the documentation 
and/or other materials provided with the distribution.

Neither the name of the copyright holder nor the names of its contributors 
may be used to endorse or promote products derived from this software without 
specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" 
AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE 
LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL 
DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; 
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY 
THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING 
NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, 
EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

------------------------------------------------------------
Novetus Launcher and Script license (MIT License)
------------------------------------------------------------

Copyright (c) 2021 Bitl

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
© 2021 GitHub, Inc.

Note: Older Novetus versions will say that they are under the GPL, but they are under the MIT license now.

Read it here https://github.com/Novetus/Novetus_src/blob/master/LICENCE or in LICENCE.txt.

------------------------------------------------------------
query.php license (GPL 3.0)
------------------------------------------------------------

This file is part of Novetus, but unlike the rest of the program where it is under the MIT license, 
this file is under the GPL 3.0 license.

Novetus's query.php is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Novetus's query.php is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with Novetus's query.php.  If not, see <https://www.gnu.org/licenses/>.

Read it here https://github.com/Novetus/Novetus_src/blob/master/LICENSE-QUERY-PHP or in LICENSE-QUERY-PHP.txt.