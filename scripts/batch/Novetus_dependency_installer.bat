@ECHO OFF
setlocal
cd /d %~dp0
:REDISTINSTALLER
TITLE NOVETUS DEPENDENCY INSTALLER
ECHO ---------------------------------------------------------------------------
ECHO              NhhhhhhhhhhN                            hsssoosssd            
ECHO             NyyyyhhhhyyhdN                          hoo++++++h            
ECHO             NyyyyyhhhhyyyhN                         hoo+++///h            
ECHO             NssssyyyyyhyyyhdN                       hoo+++///h            
ECHO             Nsossssyyyyyyyyyhm                      hsoo++///h            
ECHO             NsssssssssyyyyyyyhdN                    hssoo++//h            
ECHO             Nsssssssssssyyyyhhhdm                   hsssoo+++h            
ECHO             NyssssssssysyyyyhhhhhdN NNNNNmmmmdddddhhysoooo+++ymNN         
ECHO             Nhyyssssssmdyyyyhhhhhyhyysssssooooooooosssooooo++osssyhdmN    
ECHO             Nhyyyyssssddhyyyhhyyysssosyyhhhddmmmmmmmyooooo+++yhyssoosydN  
ECHO             NdhhyyyysssoosyhyyyssooooydN            y++++++++hN NNdhsoohN 
ECHO           NmdhhhyyyysyhddmNmhyssooooooshN           s++++++++h      doosm 
ECHO       NNdhysoyyyysssssm     NdsoooooooooydN         s/////+++h    NmyooyN 
ECHO     Ndysoosyhyysssooosm       myooooooososhN        s///////+h  Ndhsoshm  
ECHO   mhsoosydmNmysooooooom        NdssssssssooydN      s////////hdhssosydN   
ECHO NhsosydN    Nsoooo+++om          mhssssssoooohm     y++/////+soosyhmN     
ECHO NhoosdN      Nsooo+++oom           NdyssssooooosdN  Ny++++oooosydmN        
ECHO msood        Noooooooosm             Nhssooooooooydhysoooo++++hN           
ECHO NhooshmN     Nsoooooossm             Nmhsoooooooooooso++////++h            
ECHO Nhysossyhhdddoooooooosdmmmmmdddhhhyysssoooooooooo++ss++//++++h            
ECHO   Nmhyyssooooooooooooosssooooooossssyyhhdds+++++++++++++++oood            
ECHO       NNmmmddssooooooohhhhddddmmmNNNN     Ny+++++++++++++osssd            
ECHO             Nyyssssoosm                    Nds+++++++++oosssyd            
ECHO             Nhyyysssssm                      Nho++++++oossssyd            
ECHO             Nyyyyyssssm                        ms+++++ooossssd            
ECHO             Nyyyssssssm                         Nho+++ooossssd            
ECHO             Nhyyyyyyyym                           mysssssyyyyd
ECHO ---------------------------------------------------------------------------
ECHO.
ECHO NOVETUS DEPENDENCY INSTALLER
ECHO.
ECHO Please install the following if you haven't already:
ECHO 1 - Microsoft .NET Framework 4.5.1 (REQUIRED for the Novetus Launcher, not needed for Windows 8.1 and up.)
ECHO 2 - Microsoft .NET Framework 2.0 (REQUIRED for the ROBLOX Script Generator SDK tool)
ECHO 3 - Windows Media Player (REQUIRED for 2010L+)
ECHO 4 - Exit
ECHO.
SET /P M=Choose an option by typing the number corresponding to which depenency you want to install: 
IF %M%==1 goto net4
IF %M%==2 goto net2
IF %M%==3 goto mfp
IF %M%==4 EXIT
EXIT

:net2
CLS
echo Installing Microsoft .NET Framework 2.0...
reg Query "HKLM\Hardware\Description\System\CentralProcessor\0" | find /i "x86" > NUL && set OS=32BIT || set OS=64BIT

if %OS%==32BIT "%CD%/_redist/NET Framework/NetFx20SP2_x86.exe"
if %OS%==64BIT "%CD%/_redist/NET Framework/NetFx20SP2_x64.exe"
pause
goto REDISTINSTALLER

:net4
CLS
echo Installing Microsoft .NET Framework 4.5.1...
"%CD%/_redist/NET Framework/NDP451-KB2858728-x86-x64-AllOS-ENU.exe"
pause
goto REDISTINSTALLER

:mfp
CLS
echo Please install the Windows Media Player through Windows' "Add a feature" utility.
echo Windows 7+: Go to the Control Panel, Programs and Features, go to "Turn Windows features on and off", and enable Windows Media Player in Media Features.
echo Windows 10+: Go to the Settings app, Apps, Optional Features, then search "Windows Media Player", then select it and press Install.
pause
goto REDISTINSTALLER