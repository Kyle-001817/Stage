!define PROJECT_NAME "BTP"
!define PROJECT_VERSION "1.0.0"
!define INSTALL_DIR "$PROGRAMFILES\BTP"

Outfile "BTP_Setup.exe"
InstallDir "$INSTALL_DIR"

Page directory
Page instfiles
UninstPage uninstConfirm
UninstPage instfiles

Section "Installer"
  SetOutPath "$INSTDIR"
  File /r "D:\ITU\Stage\BTP\*.*"

  CreateShortcut "$DESKTOP\BTP.lnk" "$INSTDIR\BTP.exe"

  CreateDirectory "$SMPROGRAMS\BTP"
  CreateShortcut "$SMPROGRAMS\BTP\BTP.lnk" "$INSTDIR\BTP.exe"
  CreateShortcut "$SMPROGRAMS\BTP\Uninstall BTP.lnk" "$INSTDIR\Uninstall.exe"

  WriteUninstaller "$INSTDIR\Uninstall.exe"
SectionEnd
