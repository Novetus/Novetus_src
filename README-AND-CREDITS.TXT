---------------------------------------------------------------------------------------------
Welcome to Novetus!
---------------------------------------------------------------------------------------------

Official links:
https://bitl.itch.io/novetus
https://gamejolt.com/games/Novetus/375720
https://archive.org/details/novetus-13-archive
https://github.com/Novetus/Novetus_src

---------------------------------------------------------------------------------------------
INFORMATION
---------------------------------------------------------------------------------------------

[WHAT IS NOVETUS?]
​Novetus is a free and open source (FOSS) multi-version ROBLOX client launcher 
built to allow the user to run LAN and Internet servers.
						
Novetus was made as a successor to my old RBXLegacy project, 
with the addition of new features that enhance the launcher for mod development, 
player customization, and overall usability.

Novetus is based on RBXLegacy 1.16.2, however it uses some features from RBXLegacy 1.18,
such as extended customization, better layout, improved networking features, avatar
previewing, and more.

WARNING: 
If Hamachi is on, you won't be able to join "localhost" or any other local or private IP address! In order to use Novetus on LAN, 
you must either turn off Hamachi by pressing the "Turn Off" button, or by turning one of your Hamachi networks online if you own a network.

NOTE:
Make sure the server you are trying to join is in your region of the world (I.E West US, East US, UK, etc), if it is not you may experience major network lag issues.
(i.e. if you are in Arizona and someone hosts a server in Florida, you will not have a fun time because it will lag a lot.)
If you have a problem where you can't see your character, REGENERATE YOUR PLAYER ID THEN REJOIN THE SERVER.
If you tried to connect to the server and you get an error, REJOIN THE SERVER.

NOTE: 
If you get an error trying to open the launcher, install the redistributables using the Novetus_Dependency_Installer.exe located in the "data" folder.
You must also have DirectX 9.0c installed and a graphics card/sound card that supports it.

---------------------------------------------------------------------------------------------
THE BASICS
---------------------------------------------------------------------------------------------

[HOSTING]

How to host a server:

1. Port forward your specified port in the OPTIONS tab as UCP. If you do not know how to port forward, there are plenty of tutorials on the internet for this.
2. Open the Novetus Launcher.
3. Select the client you want.
4. Select the "START SERVER" tab.
5. Select the name of the map you want to play, then press the "START SERVER" button. The Studio window should load with the map you chose.
6. To share your server with friends, send them your public/external IP address (Google ip).

How to host a Local/LAN server:

1. First, you must know your LAN IPv4 address from ipconfig. If you don't know your LAN IPv4 address, there are plenty of tutorials on the internet for this.
2. Open the Novetus Launcher.
3. Select the client you want.
4. Select the "START SERVER" tab.
5. Select the name of the map you want to play, then press the "START SERVER" button. The Studio window should load with the map you chose.
6. To share your server with friends, tell them your LAN IPv4 address.

How to host a Hamachi server or any other LAN tunneler software:

1. Make sure you have Hamachi or your LAN tunneler software installed and you made a network already. If you do not know how, there are plenty of tutorials on the internet for this.
2. Open the Novetus Launcher.
3. Select the client you want.
4. Select the "START SERVER" tab.
5. Select the name of the map you want to play, then press the "START SERVER" button. The Studio window should load with the map you chose.
6. To share your server with friends, send them your Hamachi IPv4 address located above your computer's name on the Hamachi window. 
You must also share the network ID and password to your Hamachi network.

[CONNECTING]

How to connect via IP (LAN/Online):

1. Open the Novetus Launcher.
2. Select the client the server uses.
3. Select the "JOIN SERVER" tab.
4. Type in the IP address that is shared to you into the "IP Address" box, and then press the "JOIN SERVER" button.
5. You should be able to join the server.

How to connect via Hamachi or any other LAN tunneler software:

1. Open the Novetus Launcher.
2. Select the client the server uses.
3. Select the "JOIN SERVER" tab.
4. Type in the IPv4 Address that is in the title of the Hamachi/LAN tunneler network you are in into the "IP Address" box, and then press press the "JOIN SERVER" button.
5. You should be able to join the server.

[KNOWN ISSUES AND SOLUTIONS]

NOVETUS DOESN'T LOAD!
Install .NET Framework 4.6 (in the _redist folder or through the Dependency Installer)

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

[ALT-TABBING OUT OF A CLIENT DOESN'T LET ME USE THE TASK BAR OR ANYTHING!]
Press Ctrl+Alt+Del and click on a button (i.e, cancel or task manager) a couple times.

[NOVETUS CRASHES ON WINDOWS XP EVEN WITH .NET FRAMEWORK 4 INSTALLED!]
Install the KB2468871 update for NET Framework 4 (in the _redist folder or through the Dependency Installer)

[ID=15 ERROR!]
There are two instances this error pops up:

- Joining a server. This happens if the server is set to "localhost" in the launcher or the server is down:

Server joining:
If you're trying to join the server, make sure the IP and port have been posted in the launcher. Don't just press the play button expecting to join a server.
If you still can't join, contact the server host regarding the server's status.

Local play:
If you're trying to host a local session of Novetus on your machine, start a Server before pressing play, or use the Play Solo mode.

- Server hosting issues (an issue with port forwarding, UPnP not being set up on the router, etc.):

Port forwarding:
Make sure the configured ports in the launcher are forwarded for UDP and TCP on your router.
Make sure the configured ports in the launcher are forwarded through Windows Firewall and any firewall provided by an antivirus.

UPnP:
Read your router's manual regarding how to set up UPnP if it isn't set up. Most routers may need this option to be enabled in the admin panel.

VPN/Tunneling:
Please read the documentation provided by your VPN or tunneling service regarding server hosting or port forwarding.

---------------------------------------------------------------------------------------------
THE ADVANCED STUFF
---------------------------------------------------------------------------------------------

[MASTER SERVERS]

HOW TO HOST A MASTER SERVER:

1. Find your IPv4 IP, Hamachi IP, or Playit.gg IP, you will use this to share your server. You may need to port forward the port "80" on TCP and UDP.
2. Install a web server like WAMPServer.
3. After installing it, start up the WAMPServer application. Wait until the "W" on your taskbar goes green.
4. Left click the "W", then click on the "www directory" option. Windows Explorer will load with a folder named "www".
5. Copy the query.php from Novetus' directory over to the opened "www" folder.
6. Your master server is now up! You can now share your master server with other users.

WHAT IS A MASTER SERVER?
A master server hosts a list of servers for a user to join. They can be web based or use Novetus' server browser.

IS A MASTER SERVER REQUIRED WHEN HOSTING A SERVER?
No.

IS A MASTER SERVER REQUIRED WHEN JOINING A SERVER?
No.

DO I NEED MASTER SERVER INFO FOR USING THE SERVER BROWSER OR FOR LISTING MY SERVER ON A MASTER SERVER'S LIST?
Yes.

SERVERS AREN'T LOADING IN THE SERVER BROWSER!
Make sure the IP is not set to localhost. If it is, find a Master Server provider if you wish to use the Server Browser. 
Otherwise, check on your Master Server provider's hosting status.

I WANT TO HOST A MASTER SERVER!
Host the list.php and delist.php files in the misc/masterserver or masterserver folders. 
Make sure you set up your server to create and write to files as the files create and edit a serverlist.txt used by Novetus' Master Server.

[WEB PROXY]

WHAT IS THE WEB PROXY?
A web proxy, in layman's terms, is a server that redirects information from one place and puts it into another place. 
It redirects web traffic to a different location and in some cases can act as a gateway to different sites.
Novetus uses the web proxy to enable more complicated features that cannot be accomplished with scripts, such as badges and asset redirection.
It is fully optional as well!

WHAT FEATURES DOES THE WEB PROXY HAVE?
At the moment, it has:

- Local asset redirection.
- Online asset redirection. Currently ROBLOX-created assets only.
- Badge Web API support (AwardBadge and HasBadge).
- Various static webpage support, including the original ROBLOX help documentation.

More features will be added in the future.

HOW DO I ACTIVATE IT?
The launcher will have a web proxy toggle switch in the "Options" menu.
When enabling the web proxy, Novetus will locally create a certificate upon startup that ensures the proxy's functionality. 
Novetus will not send any user data to anyone, as everything involving the web proxy is entirely local to this computer.
If you have any issue connecting to other web sites, including Roblox, closing Novetus or typing 'proxy off' into Novetus' console will fix it in most instances.
Upon pressing 'Yes', Windows will ask you for permission to install the certificate.
This certificate is required for the web proxy to work, and pressing "No" in the dialog will break its functionality.
You can change this option at any time by typing 'proxy disable' or 'proxy on' in the Novetus console.
NOTE: The Web proxy feature requires an Internet connection to function properly.

CAN I EXTEND THE WEB PROXY?
Yes! There will be more information in this readme's "Modding" section.

---------------------------------------------------------------------------------------------
MODDING
---------------------------------------------------------------------------------------------

TODO

---------------------------------------------------------------------------------------------
CREDITS AND LICENSES:
---------------------------------------------------------------------------------------------

ROBLOX and the ROBLOX Clients were made by the ROBLOX Corporation.
The ROBLOX Corporation does not support or endorse the creation of Novetus.
Bitl Development Studio is not affiliated with the ROBLOX Corporation or its subsidiaries.
Bitl Development Studio does not own the majority of the places or items included with Novetus.
Bitl Development Studio does not support the use of AI. No AI was used in Novetus' development.
Novetus is not associated with Novetus Engineering LLC.
This item is not authorized for release on Steam or any other game storefront nor should it be paid for.​ If you paid for Novetus, get a refund immediately.
Novetus uses the majority of the Whimsee's Map Pack in the "full" version. Credits go to Whimsee and many other people for making that pack possible.
Thank you to everyone who has contributed a map, item, or client including cole and many other people. 
LUA scripts were used to build a client that can connect to LAN and the Internet.
The LUA scripts used were borrowed from the RBXPri client and merged into 1 single script.
All credit for the LUA code included with the RBXPri client goes to the RBXPri team.
All credit for the LUA code used with "non-modern" clients goes to Scripter John and EnergyCell.
All credit for the LUA code used for character customization goes to RBXBanLand.
Parts of the codebase use bits and pieces of code from Stack Overflow, MSDN Forums, the Novetus GitHub Pull Requests and Codeproject.
The original concept for the Diogenes editor was suggested by Carrot. The concept code was then modified to be smaller, more efficient, and more customizable.
The script signing and key generation code was created by Carrot, but was slimmed down to work properly in Novetus and modified to be more efficient.
ObjToRBXMesh was made by coke. Modified to support 1.00 and 1.01. (https://github.com/Novetus/ObjToRBXMesh)
Roblox Legacy Place Converter was made by BakonBot. (https://github.com/BakonBot/legacy-place-converter)
ROBLOX Script Generator was made by S. Costeira.
Novetus uses a fork of MaximumADHD's Roblox-File-Format library. (https://github.com/Novetus/Roblox-File-Format-Net4.5.1)
Thank you to NT_x86 for helping me with security fixes.
Thank you XlXi for the idea of the original logo. This logo was remade in newer verions in higher quality.
Thank you Nukley for the idea of the Splash Tester. 
Credits go to matboff for getting 2011M corescripts working and helping me with 2011E corescripts.
Credits to Hazelnut (creator of JRBX) for the buttons used in the Stylish style.
Credits go to davcs86 for the HWID generation code (https://github.com/davcs86/csharp-uhwid)
Credits go to matboff and Sodikm for parts of the web proxy code.
All credits for the used pieces of code go to the respective authors.
Music Used:
"Antonymph" by Vylet Pony 

---------------------------------------------------------------------------------------------
Mark James' Silk icon set 1.3
---------------------------------------------------------------------------------------------

The Discord Rich Presence icons and the SDK icons used for this application use Mark James' Silk icon set 1.3.
https://web.archive.org/web/20200328175105/http://www.famfamfam.com/lab/icons/silk/

---------------------------------------------------------------------------------------------
Novetus Launcher, Script and query.php license (MIT License)
---------------------------------------------------------------------------------------------

MIT License

Copyright (c) 2025 Bitl Development Studio/Bitl

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

NOTE: In previous versions of Novetus, certain lines discussed about retroactive changes to the license 
following the 1.3/new 1.4 snapshots/1.4 (MIT) and the old 1.4 snapshots (GPL 3.0) license changes. 

These changes are legally NOT supposed to be retroactive due to the nature of open source licenses. 
ALL FUTURE, PRESENT AND PAST VERSIONS OF NOVETUS USE THE LICENSE THEY CAME WITH AND NO LICENSE IS APPLIED RETROACTIVELY.