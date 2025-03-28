; Script generated by the HM NIS Edit Script Wizard.

; HM NIS Edit Wizard helper defines
!define PRODUCT_NAME "Novetus Dependencies"
!define PRODUCT_VERSION "1.0"
!define PRODUCT_PUBLISHER "Bitl Development Studio"

; MUI 1.67 compatible ------
!include "MUI.nsh"

; MUI Settings
!define MUI_ABORTWARNING
!define MUI_ICON "G:\Projects\GitHub\Novetus\Novetus_src\Graphics\NovetusIcon.ico"
!define MUI_WELCOMEFINISHPAGE_BITMAP "G:\Projects\GitHub\Novetus\Novetus_src\Graphics\Novetus_Installer_WizardImage.bmp"

; Welcome page
!insertmacro MUI_PAGE_WELCOME
; Components page
!insertmacro MUI_PAGE_COMPONENTS
; Instfiles page
!insertmacro MUI_PAGE_INSTFILES
; MediaPlayer
!define MUI_PAGE_HEADER_TEXT "Windows Media Player"
!define MUI_PAGE_HEADER_SUBTEXT "If you don't have Windows Media Player binaries installed, please follow these directions."
!define MUI_LICENSEPAGE_TEXT_TOP "Instructions:"
!define MUI_LICENSEPAGE_TEXT_BOTTOM "Please make sure you have this dependency installed before continuing."
!define MUI_LICENSEPAGE_BUTTON "Next >"
!insertmacro MUI_PAGE_LICENSE "WMP_instructions.txt"
; Finish page
!insertmacro MUI_PAGE_FINISH

; Language files
!insertmacro MUI_LANGUAGE "English"

; MUI end ------

Name "${PRODUCT_NAME}"
OutFile "Novetus_Dependency_Installer.exe"
ShowInstDetails show

Section ".NET Framework 4.6" SEC01
  DetailPrint "Installing .NET Framework 4.6..."
  ExecWait '"$EXEDIR\_CommonRedist\DotNet\4.6\NDP46-KB3045557-x86-x64-AllOS-ENU.exe"  /q /norestart'
SectionEnd

Section ".NET Framework 2.0" SEC02
  DetailPrint "Installing .NET Framework 2.0..."
  ExecWait '"$EXEDIR\_CommonRedist\DotNet\2.0SP2\NetFx20SP2_x86.exe"  /q'
  ExecWait '"$EXEDIR\_CommonRedist\DotNet\2.0SP2\NetFx20SP2_x64.exe"  /q'
SectionEnd

Section "DirectX" SEC03
  DetailPrint "Installing DirectX..."
  ExecWait '"$EXEDIR\_CommonRedist\DirectX\Jun2010\DXSETUP.exe"  /silent'
SectionEnd

Section "Visual C++ 2005 Redistributables" SEC04
  DetailPrint "Installing Visual C++ 2005 Redistributables..."
  ExecWait '"$EXEDIR\_CommonRedist\vcredist\2005\vcredist_x86sp1cur.exe"  /q /r:n'
  ExecWait '"$EXEDIR\_CommonRedist\vcredist\2005\vcredist_x86SP1.exe"  /q /r:n'
  ExecWait '"$EXEDIR\_CommonRedist\vcredist\2005\vcredist_x86SP1ATL.exe"  /q /r:n'
  ExecWait '"$EXEDIR\_CommonRedist\vcredist\2005\vcredist_x86SP1MFC.exe"  /q /r:n'
SectionEnd

Section "Visual C++ 2008 Redistributables" SEC05
  DetailPrint "Installing Visual C++ 2008 Redistributables..."
  ExecWait '"$EXEDIR\_CommonRedist\vcredist\2008\vcredist_x86.exe"  /q /norestart'
  ExecWait '"$EXEDIR\_CommonRedist\vcredist\2008\vcredist_x86sp1.exe"  /q /norestart'
  ExecWait '"$EXEDIR\_CommonRedist\vcredist\2008\vcredist_x86sp1cur.exe"  /q /norestart'
SectionEnd

; Section descriptions
!insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
  !insertmacro MUI_DESCRIPTION_TEXT ${SEC01} "Used for running the Novetus Launcher."
  !insertmacro MUI_DESCRIPTION_TEXT ${SEC02} "Used for running the Novetus SDK's Script Generator application."
  !insertmacro MUI_DESCRIPTION_TEXT ${SEC03} "Used for running all clients."
  !insertmacro MUI_DESCRIPTION_TEXT ${SEC04} "Used for running 2007 clients."
  !insertmacro MUI_DESCRIPTION_TEXT ${SEC05} "Used for running 2008 clients and above."
!insertmacro MUI_FUNCTION_DESCRIPTION_END