@ECHO OFF
setlocal
cd /d %~dp0
:MENU
TITLE NOVETUS LEGACY LAUNCHER
CLS
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
ECHO NOVETUS LEGACY LAUNCHER
ECHO.
ECHO 1 - Play
ECHO 2 = Install Required Dependencies
ECHO 3 - Novetus SDK
ECHO 4 - Novetus CMD
ECHO 5 - Novetus CMD Help
ECHO 6 - Install URI
ECHO 7 - Exit
ECHO.
SET /P M=Choose an option by typing the number corresponding to which utility you want to launch: 
IF %M%==1 CLS
IF %M%==1 start "" "%CD%/bin/Novetus.exe"
IF %M%==1 EXIT

IF %M%==2 CLS
IF %M%==2 call "%CD%/Novetus_dependency_installer.bat"

IF %M%==3 CLS
IF %M%==3 start "" "%CD%/bin/Novetus.exe" -sdk
IF %M%==3 EXIT

IF %M%==4 CLS
IF %M%==4 "bin/NovetusCMD.exe"

IF %M%==5 CLS
IF %M%==5 "bin/NovetusCMD.exe" -help

IF %M%==6 CLS
IF %M%==6 start "" "%CD%/bin/NovetusURI.exe"
IF %M%==6 EXIT

IF %M%==7 EXIT
EXIT
