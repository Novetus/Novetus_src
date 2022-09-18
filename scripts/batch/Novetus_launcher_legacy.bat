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
ECHO 2 - Play with Console
ECHO 3 = Install Required Dependencies
ECHO 4 - Novetus SDK
ECHO 5 - Novetus Console (Server Mode)
ECHO 6 - Novetus Console Help
ECHO 7 - Install URI
ECHO 8 - Exit
ECHO.
SET /P M=Choose an option by typing the number corresponding to which utility you want to launch: 
IF %M%==1 CLS
IF %M%==1 start "" "%CD%/bin/Novetus.exe" -nocmd
IF %M%==1 EXIT

IF %M%==2 CLS
IF %M%==2 start "" "%CD%/bin/Novetus.exe"
IF %M%==2 EXIT

IF %M%==3 CLS
IF %M%==3 call "%CD%/Novetus_dependency_installer.bat"

IF %M%==4 CLS
IF %M%==4 start "" "%CD%/bin/Novetus.exe" -sdk
IF %M%==4 EXIT

IF %M%==5 CLS
IF %M%==5 start "" "%CD%/bin/Novetus.exe" -cmdonly -cmdmode
IF %M%==5 EXIT

IF %M%==6 CLS
IF %M%==6 start "" "%CD%/bin/Novetus.exe" -cmdonly -help
IF %M%==6 EXIT

IF %M%==7 CLS
IF %M%==7 start "" "%CD%/bin/NovetusURI.exe"
IF %M%==7 EXIT

IF %M%==8 EXIT
EXIT
